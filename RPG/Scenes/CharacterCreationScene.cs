using DbService;
using GeneralUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class CharacterCreationScene : DefaultScene
    {
        private static readonly Point NAME_POS = new Point(24, 6);
        private static readonly Point NAME_SIZE = new Point(31, 3);

        private static readonly List<Point> _dicesPos = new List<Point>()
        {
            new Point(11, 8),
            new Point(21, 11),
            new Point(36, 13),
            new Point(51, 11),
            new Point(61, 8)
        };

        private readonly string[] _invalidName = new string[]
        {
            "Un héros sans nom ?\nWow ! Quelle originalité..."
        };

        private readonly Dice _dice6;
        private readonly List<int> _criteriaValues;

        private readonly ContextualMenu _goToOriginMenu;

        private string _name;
        private int _idRace;

        /* ======== */

        private readonly ContextualMenu _originsMenu;

        public CharacterCreationScene()
        {
            _dice6 = new Dice();
            _criteriaValues = new List<int>();
            _goToOriginMenu = new ContextualMenu(y: 23);
            _goToOriginMenu.AddMenuItem(" > Choisir l'origine du héros < ", () => { });
        }

        public override void ExecuteScene()
        {
            DisplayTools.WriteInWindowCenter("CRÉATION DE TON HÉROS", y: 4, animated: true);
            AskCharacterName();
            RollForCriterias();
            FinalizeCriteriaValues();
            ShowCriterias();
            _goToOriginMenu.Execute();
            Console.Clear();

            SavePlayer();
            Game.ActiveScene = new TitleMenuScene();
        }

        
        private void AskCharacterName()
        {
            DisplayTools.WriteInBufferAt(DisplayTools.Yellow, 0, 0);
            DisplayTools.PrintBox(NAME_POS.X, NAME_POS.Y, NAME_SIZE.X, NAME_SIZE.Y, DisplayTools.BorderStyle.Double);
            DisplayTools.WriteInWindowAt($"{DisplayTools.Reset}Nom :", NAME_POS.X + 2, NAME_POS.Y + 1);

            _name = "";
            bool valid = false;
            while (!valid)
            {
                _name = UserInputUtils.GetInputAt(NAME_POS.X + 8, NAME_POS.Y + 1);
                valid = !string.IsNullOrEmpty(_name);

                if (!valid)
                {
                    DisplayTools.WriteInWindowAnimated(_invalidName);
                }
            }
        }

        private void RollForCriterias()
        {
            Point rollPosition;

            for (int i = 0; i < GameRules.CRITERIAS.Length; i++)
            {
                rollPosition = _dicesPos[i];
                DisplayTools.WriteInWindowAt(GameRules.CRITERIAS[i], rollPosition.X + 2, rollPosition.Y - 1);
                _criteriaValues.Add(_dice6.RollWithAnimation(rollPosition.X, rollPosition.Y));
            }
        }

        private void SavePlayer()
        {
            try
            {
                using RpgContext rpgContext = new RpgContext();

                Inventaire inventory = new Inventaire()
                {
                    Titre = "Baluchon",
                    Slots = 10
                };
                rpgContext.Inventaire.Add(inventory);
                rpgContext.SaveChanges();

                Joueur newPlayer = new Joueur()
                {
                    Courage = _criteriaValues[0],
                    Intelligence = _criteriaValues[1],
                    Charisme = _criteriaValues[2],
                    Adresse = _criteriaValues[3],
                    Force = _criteriaValues[4],
                    // temporaire
                    PointsVie = 0,
                    EnergieAstrale = 0,
                    Experience = 0,
                    Niveau = 0,
                    IdMapCourante = 2,
                    IdRace = 1,
                    Nom = _name,
                    Or = 0,
                    Argent = 0,
                    Debilibuck = 0,
                    Destin = 0,
                    ManqueDeBol = 0,
                    IdInventaire = inventory.IdInventaire
                };

                rpgContext.Joueur.Add(newPlayer);
                rpgContext.SaveChanges();
            }
            catch
            {

            }
        }

        private void FinalizeCriteriaValues()
        {
            for (int i = 0; i < _criteriaValues.Count; i++)
            {
                _criteriaValues[i] += GameRules.TO_ADD_TO_CRITERIA;
            }
        }

        private void ShowCriterias()
        {
            string[] template = ResourcesUtils.GetTemplate($"{ResourcesUtils.UI_PATH}/criterias_creation_template.txt");
            int i = 0;
            const int gap = 15;

            DisplayTools.WriteInWindowAt(template, 3, 18);

            foreach (string crit in GameRules.CRITERIAS_NAME)
            {
                DisplayTools.WriteInWindowAt(crit, 5 + (i * gap), 19);
                DisplayTools.WriteInWindowAt(_criteriaValues[i].ToString(), 15 + (i * gap), 20);
                i++;
            }
        }
    }
}
