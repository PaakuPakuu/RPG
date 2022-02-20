using GeneralUtils;
using DbService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG
{
    public class GameSavesScene : DefaultScene
    {
        private readonly RpgContext _rpgContext;

        private readonly int MAX_SAVES = 5;

        private readonly Point SAVES_POS = new Point(7, 5);
        private readonly int SAVES_GAP = 4;

        private readonly string _template;

        private readonly ContextualMenu _profilesMenu;
        private readonly ContextualMenu _profileActionMenu;
        private readonly ContextualMenu _confirmRemoveMenu;

        private Joueur _selectedPlayer;

        public GameSavesScene()
        {
            _rpgContext = new RpgContext();

            _template = string.Join("\n", ResourcesUtils.GetFileLines($"{ResourcesUtils.UI_PATH}/save_template.txt"));

            _profilesMenu = new ContextualMenu(x: SAVES_POS.X, y: SAVES_POS.Y, padding: SAVES_GAP - 1, selectedStyle: ContextualMenu.SelectedStyle.Reversed);

            _profileActionMenu = new ContextualMenu(x: ScreenWidth - 12, y: ScreenHeight - 7, padding: 1, selectedStyle: ContextualMenu.SelectedStyle.Arrow);
            _profileActionMenu.AddMenuItem("Charger", LoadSelectedPlayerGame);
            _profileActionMenu.AddMenuItem("Supprimer", AskToRemove);
            _profileActionMenu.AddMenuItem("Retour", RestartThisScene);

            _confirmRemoveMenu = new ContextualMenu(padding: 1, centered: true, selectedStyle: ContextualMenu.SelectedStyle.Dashes);
            _confirmRemoveMenu.AddMenuItem("Oui", RemoveSave);
            _confirmRemoveMenu.AddMenuItem("Annuler", RestartThisScene);
        }

        public override void ExecuteScene()
        {
            List<Joueur> saves = _rpgContext.Joueur.Take(MAX_SAVES).ToList();
            string level;
            int menuY = 0;

            foreach (Joueur player in saves)
            {
                level = player.Niveau.ToString().PadLeft(2, '0').PadRight(3);
                DisplayTools.WriteInWindowAt(string.Format(_template, level), SAVES_POS.X - 3, SAVES_POS.Y - 1 + (SAVES_GAP * menuY));
                _profilesMenu.AddMenuItem(player.Nom, () => { ExecuteProfileAction(player); });

                menuY++;
            }

            if (saves.Count < MAX_SAVES)
            {
                _profilesMenu.AddMenuItem($"{DisplayTools.Yellow}Nouvelle partie{DisplayTools.Reset}", LaunchCharacterCreationScene);
            }

            _profilesMenu.AddMenuItem("Retour", LaunchTitleMenuScene);

            _profilesMenu.Execute();
        }

        #region Actions

        private void LoadSelectedPlayerGame()
        {
            //DisplayTools.WriteInDialogBox(new string[]
            //{
            //    "Vous vous apprétez à créer un nouveau personnage."
            //});
            //DisplayTools.WriteInDialogBox(new string[]
            //{
            //    "Cependant, le développeur n'a pas encore implémenté cette",
            //    "possibilité..."
            //});
            //DisplayTools.WriteInDialogBox(new string[]
            //{
            //    "HONTE À LUI !"
            //});
            //DisplayTools.WriteInDialogBox(new string[]
            //{
            //    "Nan j'déconne, il fait de son mieux. Calme-toi."
            //});

            //RestartThisScene();

            Game.Player = Manager.GetPlayer(_selectedPlayer);
            Game.CurrentMap = Manager.GetMap(_rpgContext.Map.Single(m => m.IdMap == _selectedPlayer.IdMapCourante));
            Game.ActiveScene = new GameScene();
        }

        private void ExecuteProfileAction(Joueur player)
        {
            _selectedPlayer = player;
            _profileActionMenu.Execute();
        }

        private void AskToRemove()
        {
            DisplayTools.PrintBox(-1, -1, 15, 5, DisplayTools.BorderStyle.SimpleCurved);
            _confirmRemoveMenu.Execute();
        }

        private void RemoveSave()
        {
            _rpgContext.Joueur.Remove(_selectedPlayer);
            _rpgContext.Inventaire.Remove(_rpgContext.Inventaire.Single(i => i.IdInventaire == _selectedPlayer.IdInventaire)); // IdInventaireNavigation = null ??
            _rpgContext.SaveChanges();
            RestartThisScene();
        }

        private void LaunchTitleMenuScene() => Game.ActiveScene = new TitleMenuScene();

        private void RestartThisScene() => Game.ActiveScene = new GameSavesScene();

        private void LaunchCharacterCreationScene() => Game.ActiveScene = new CharacterCreationScene();

        #endregion
    }
}
