using DbService;
using GeneralUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGEditor
{
    public class OriginsScene : Scene
    {
        #region Box positioning values

        private static readonly Point ORIGINS_BOX_POS = new Point(1, 1);
        private static readonly Point ORIGINS_BOX_SIZE = new Point(28, 30);
        private static readonly Point TABLE_BOX_POS = new Point(30, 1);
        private static readonly Point TABLE_BOX_SIZE = new Point(68, 30);
        private static readonly Point INPUT_BOX_SIZE = new Point(10, 3);
        private static readonly Point INPUT_BOX_POS = TABLE_BOX_POS + TABLE_BOX_SIZE - INPUT_BOX_SIZE - new Point(3, 1);
        private static readonly Point MESSAGE_BOX_SIZE = new Point(TABLE_BOX_SIZE.X, 5);
        private static readonly Point MESSAGE_BOX_POS = new Point(TABLE_BOX_POS.X, TABLE_BOX_POS.Y + TABLE_BOX_SIZE.Y + 2);

        private static readonly Point INPUT_POS = INPUT_BOX_POS + new Point(7, 1);

        #endregion

        private static readonly string VALID_CRITERIA_M = "La valeur de {0} de {1} a été mise à jour avec succès.";

        // We keep connection open until user leave this scene.
        private readonly RpgContext _rpgContext;

        private readonly ContextualMenu _originsMenu;
        private readonly ContextualMenu _criteriasMenu;

        private readonly List<Origine> _origins;

        private int _selectedOriginId;

        public OriginsScene()
        {
            _rpgContext = new RpgContext();

            _originsMenu = new ContextualMenu(x: 4, y: 3, padding: 1, selectedStyle: ContextualMenu.SelectedStyle.Underlined);
            _originsMenu.AddMenuItem($"{DisplayTools.Green}Retour{DisplayTools.Reset}", LaunchMenusScene);

            _criteriasMenu = new ContextualMenu(x: TABLE_BOX_POS.X + 4, y: TABLE_BOX_POS.Y + TABLE_BOX_SIZE.Y - 3, padding: 2, horizontal: true);
            _criteriasMenu.AddMenuItem("Retour", ExecuteOriginsMenu);
            foreach (string crit in GameRules.CRITERIAS)
            {
                _criteriasMenu.AddMenuItem(crit, () =>
                {
                    EnterValueForCriteria(crit);
                });
            }

            _origins = EditorFeatures.GetOrigins();
        }

        public override void ExecuteScene()
        {
            foreach (Origine origin in _origins)
            {
                _originsMenu.AddMenuItem(origin.Nom, () => {
                    _selectedOriginId = origin.IdOrigine;
                    GoToOriginTable(origin);
                });
            }

            DisplayTools.PrintBox(ORIGINS_BOX_POS.X, ORIGINS_BOX_POS.Y, ORIGINS_BOX_SIZE.X, ORIGINS_BOX_SIZE.Y, DisplayTools.BorderStyle.Simple);

            //_originsMenu.AddMenuItem("\n\nPage suivante", () => { });
            ExecuteOriginsMenu();
        }

        private void AskCriteria(bool isMin, int minCriteria, out int value, out string message)
        {
            DisplayTools.WriteInWindowAt((isMin ? "Min : " : "Max : "), INPUT_BOX_POS.X + 1, INPUT_BOX_POS.Y + 1);
            while (!UserInputUtils.GetNumberInOrZero(minCriteria, GameRules.MAX_CRITERIA, out value, out message))
            {
                PrintMessageBox(message);
                ResetInput();
            }
        }

        private void ResetInput()
        {
            DisplayTools.WriteInWindowAt("  ", INPUT_POS.X, INPUT_POS.Y);
            Console.SetCursorPosition(INPUT_POS.X, INPUT_POS.Y);
        }

        private void PrintMessageBox(string message)
        {
            DisplayTools.PrintBox(MESSAGE_BOX_POS.X, MESSAGE_BOX_POS.Y, MESSAGE_BOX_SIZE.X, MESSAGE_BOX_SIZE.Y, DisplayTools.BorderStyle.SimpleHeavy);
            DisplayTools.WriteInWindowAt(message, MESSAGE_BOX_POS.X + 2, MESSAGE_BOX_POS.Y + 1);
        }

        #region Actions

        private void LaunchMenusScene() => Editor.ActiveScene = new MenusScene();

        private void ShowCriteriaTable(Origine origin)
        {
            DisplayTools.PrintBox(TABLE_BOX_POS.X, TABLE_BOX_POS.Y, TABLE_BOX_SIZE.X, TABLE_BOX_SIZE.Y, DisplayTools.BorderStyle.Double);
            DisplayTools.WriteInWindowAt(origin.Nom, TABLE_BOX_POS.X + 3, TABLE_BOX_POS.Y + 2);

            // TODO : display table

        }

        private void ExecuteOriginsMenu()
        {
            Console.Clear();
            Console.CursorVisible = false;
            DisplayTools.PrintBox(ORIGINS_BOX_POS.X, ORIGINS_BOX_POS.Y, ORIGINS_BOX_SIZE.X, ORIGINS_BOX_SIZE.Y, DisplayTools.BorderStyle.Simple);
            _originsMenu.Execute();
        }

        private void GoToOriginTable(Origine origin)
        {
            ShowCriteriaTable(origin);
            _criteriasMenu.Execute();
        }

        private void EnterValueForCriteria(string criteria)
        {
            Origine origin = _rpgContext.Origine.Single(o => o.IdOrigine == _selectedOriginId);

            DisplayTools.WriteInWindowAt($"{DisplayTools.Reversed}{criteria}{DisplayTools.Reset}", INPUT_BOX_POS.X - criteria.Length - 1, INPUT_BOX_POS.Y + 1);
            DisplayTools.PrintBox(INPUT_BOX_POS.X, INPUT_BOX_POS.Y, INPUT_BOX_SIZE.X, INPUT_BOX_SIZE.Y, DisplayTools.BorderStyle.SimpleCurved);

            Console.CursorVisible = true;
            AskCriteria(true, GameRules.MIN_CRITERIA, out int min, out string message);

            DisplayTools.ClearBox(MESSAGE_BOX_POS.X, MESSAGE_BOX_POS.Y, MESSAGE_BOX_SIZE.X, MESSAGE_BOX_SIZE.Y);
            ResetInput();
            DisplayTools.WriteInWindowAt("Max : ", INPUT_BOX_POS.X + 1, INPUT_BOX_POS.Y + 1);

            AskCriteria(false, Math.Max(GameRules.MIN_CRITERIA, min), out int max, out message);

            switch (criteria)
            {
                case "COU":
                    origin.CourageMin = min;
                    origin.CourageMax = max;
                    break;
                case "INT":
                    origin.IntelligenceMin = min;
                    origin.IntelligenceMax = max;
                    break;
                case "CHA":
                    origin.CharismeMin = min;
                    origin.CharismeMax = max;
                    break;
                case "ADR":
                    origin.AdresseMin = min;
                    origin.AdresseMax = max;
                    break;
                case "FOR":
                    origin.ForceMin = min;
                    origin.ForceMax = max;
                    break;
                default: break;
            }

            message = String.Format(VALID_CRITERIA_M, criteria, origin.Nom.ToUpper());
            PrintMessageBox(message);
            _rpgContext.SaveChanges();

            Console.ReadKey();
            Console.CursorVisible = false;
            DisplayTools.ClearBox(MESSAGE_BOX_POS.X, MESSAGE_BOX_POS.Y, MESSAGE_BOX_SIZE.X, MESSAGE_BOX_SIZE.Y);

            GoToOriginTable(origin);
        }

        #endregion
    }
}
