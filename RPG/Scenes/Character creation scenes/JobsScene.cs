using DbService;
using GeneralUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG
{
    public class JobsScene : DefaultScene
    {
        private readonly string[] LEFT_LABELS = new string[]
        {
            "EV",
            "Charge",
            "2 mains",
            "Bouclier",
            "PR naturel MAX"
        };

        private readonly Point LEFT_POS = new Point(4, 12);
        private readonly Point MIDDLE_POS = new Point();
        private readonly Point RIGHT_POS = new Point();

        private readonly RpgContext _rpgContext;
        private readonly List<Metier> _jobs;

        private readonly ContextualMenu _naviguateOriginsMenu;
        private int _menuIndex;

        private readonly List<int> _criteriaValues;
        private List<(int min, int max)> _extremumValues;
        private bool _isAvailable;

        private bool _choosed;
        private int _currentJobId;
        private Metier CurrentJob { get => _jobs[_currentJobId]; }

        private readonly Joueur _player;

        public JobsScene(int idPlayer, List<int> criteriaValues)
        {
            _rpgContext = new RpgContext();
            _jobs = _rpgContext.Metier.ToList();

            _criteriaValues = criteriaValues;
            _extremumValues = new List<(int min, int max)>();

            _naviguateOriginsMenu = new ContextualMenu(x: 32, y: ScreenHeight - 2, horizontal: true, padding: 1);
            _naviguateOriginsMenu.AddMenuItem(" <──── ", PreviousOrigin);
            _naviguateOriginsMenu.AddMenuItem(" ────> ", NextOrigin);
            _naviguateOriginsMenu.AddMenuItem("\t\tSélectionner\0\0\0\0\0\0\0\0\0\0\0", Choose);

            _menuIndex = 1;

            _choosed = false;
            _currentJobId = 0;

            _player = _rpgContext.Joueur.Single(j => j.IdJoueur == idPlayer);
        }

        public override void ExecuteScene()
        {
            DisplayTools.WriteInWindowCenter($"Choix du métier de {_player.Nom}".ToUpper(), y: 1, animated: true);

            while (!_choosed)
            {
                UpdateExtremum();
                UpdateIsAvailable();
                PrintOriginTemplate();
                PrintLeftSide();
                PrintMiddleSide();
                PrintRightSide();
                PrintOriginValidation();
                _naviguateOriginsMenu.Execute(_menuIndex);
            }

            SaveJob();
            Game.ActiveScene = new LastCriteriasScene(_player.IdJoueur);
        }

        private void UpdateExtremum()
        {
            _extremumValues = new List<(int min, int max)>()
            {
                (CurrentJob.CourageMin, CurrentJob.CourageMax),
                (CurrentJob.IntelligenceMin, CurrentJob.IntelligenceMax),
                (CurrentJob.CharismeMin, CurrentJob.CharismeMax),
                (CurrentJob.AdresseMin, CurrentJob.AdresseMax),
                (CurrentJob.ForceMin, CurrentJob.ForceMax)
            };
        }

        private void PrintOriginTemplate()
        {
            string[] template = ResourcesUtils.GetFileLines($"{ResourcesUtils.UI_PATH}/origin_template.txt");
            DisplayTools.WriteInBufferAt(DisplayTools.Yellow, 0, 0);
            DisplayTools.WriteInWindowAt(template, 2, 3);
            DisplayTools.WriteInBufferAt(DisplayTools.Reset, 0, 0);

            string originTitle = Figgle.FiggleFonts.Standard.Render(CurrentJob.Nom);
            DisplayTools.WriteInWindowAt(originTitle, 4, 4);
        }

        private void PrintLeftSide()
        {
            List<string> values = new List<string>()
            {
                $"{(CurrentJob.ModificateurPv < 0 ? '-' : '+')}{CurrentJob.ModificateurPv} {DisplayTools.Red}❤{DisplayTools.Reset}",
                (CurrentJob.Charge > 0 ? $"{CurrentJob.Charge} kg" : "Selon origine"),
                (CurrentJob.DeuxMains == 0 ? "Non" : (CurrentJob.DeuxMains == 1 ? "Oui" : "Selon origine")),
                (CurrentJob.Bouclier == 0 ? "Non" : (CurrentJob.Bouclier == 1 ? "Oui" : "Selon origine")),
                CurrentJob.PointsProtection.ToString()
            };

            for (int i = 0; i < values.Count; i++)
            {
                DisplayTools.WriteInWindowAt($"{LEFT_LABELS[i]} : {values[i]}", LEFT_POS.X, LEFT_POS.Y + (i * 3));
            }
        }

        private void PrintMiddleSide()
        {
            const int gap = 4;
            string minValue;
            string maxValue;

            for (int i = 0; i < _extremumValues.Count; i++)
            {
                minValue = _extremumValues[i].min == 0 ? $"{DisplayTools.Grey}Non{DisplayTools.Reset}" : $"{DisplayTools.Green}{_extremumValues[i].min}{DisplayTools.Reset}";
                maxValue = _extremumValues[i].max == 0 ? $"{DisplayTools.Grey}Non{DisplayTools.Reset}" : $"{DisplayTools.Green}{_extremumValues[i].max}{DisplayTools.Reset}";

                DisplayTools.WriteInWindowAt($"{DisplayTools.Underlined}{GameRules.CRITERIAS_NAME[i]}{DisplayTools.Reset}", 30, 11 + (i * gap));
                DisplayTools.WriteInWindowAt($"Min: {minValue}  Max: {maxValue}", 31, 13 + (i * gap));
            }
        }

        private void PrintRightSide()
        {
            DisplayTools.WriteInWindowAt($"{DisplayTools.Underlined}Compétences héritées{DisplayTools.Reset}", 53, 11);

            List<int> skillIds = _rpgContext.CompetenceHeritee.Where(c => c.IdMetier == CurrentJob.IdMetier).Select(c => c.IdCompetence).ToList(); // To improve.
            List<string> skillNames = new List<string>();

            skillIds.ForEach(id => skillNames.Add($"• {_rpgContext.Competences.Single(s => s.IdCompetence == id).Nom}"));

            if (skillNames.Count > 0)
            {
                DisplayTools.WriteInWindowAt(skillNames.ToArray(), 53, 13);
            }
            else
            {
                DisplayTools.WriteInWindowAt($"{DisplayTools.Grey}Aucune compétence\nhéritée de ce métier{DisplayTools.Reset}", 53, 13);
            }
        }

        private void UpdateIsAvailable()
        {
            int currValue;
            bool stop = false;

            for (int i = 0; !stop && i < _extremumValues.Count; i++)
            {
                currValue = _criteriaValues[i];
                if (currValue < _extremumValues[i].min || (currValue > _extremumValues[i].max && _extremumValues[i].max != 0))
                {
                    stop = true;
                }
            }

            _isAvailable = !stop;
        }

        private void PrintOriginValidation()
        {
            string color = _isAvailable ? $"Disponible   {DisplayTools.Green}" : $"Indisponible {DisplayTools.Red}";
            DisplayTools.WriteInWindowAt($" {color}●{DisplayTools.Reset} ", ScreenWidth - 22, 3);
        }

        #region Actions

        private void NextOrigin()
        {
            _currentJobId = (_currentJobId + 1) % _jobs.Count;
            _menuIndex = 1;
        }

        private void PreviousOrigin()
        {
            _currentJobId--;
            if (_currentJobId < 0)
            {
                _currentJobId = _jobs.Count - 1;
            }
            _menuIndex = 0;
        }

        private void Choose() => _choosed = true;

        #endregion

        private void SaveJob()
        {
            _player.IdMetier = CurrentJob.IdMetier;
            _player.MaxPointsVie += (int)CurrentJob.ModificateurPv;
            _player.PointsVie = _player.MaxPointsVie;
            _rpgContext.SaveChanges();
        }
    }
}
