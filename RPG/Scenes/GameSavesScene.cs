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

        private readonly string _saveTemplate;

        private readonly ContextualMenu _profilesMenu;
        private readonly ContextualMenu _profileActionMenu;
        private readonly ContextualMenu _confirmRemoveMenu;

        private Joueur _selectedPlayer;

        public GameSavesScene()
        {
            _rpgContext = new RpgContext();

            _saveTemplate = string.Join("\n", ResourcesUtils.GetTemplate($"{ResourcesUtils.UI_PATH}/save_template.txt"));

            _profilesMenu = new ContextualMenu(x: SAVES_POS.X, y: SAVES_POS.Y, padding: SAVES_GAP - 1, selectedStyle: ContextualMenu.SelectedStyle.Reversed);

            _profileActionMenu = new ContextualMenu(x: ScreenWidth - 12, y: ScreenHeight - 7, padding: 1, selectedStyle: ContextualMenu.SelectedStyle.Arrow);
            _profileActionMenu.AddMenuItem("Charger", TempAddNewPlayer);
            _profileActionMenu.AddMenuItem("Supprimer", AskToRemove);
            _profileActionMenu.AddMenuItem("Retour", LaunchThisScene);

            _confirmRemoveMenu = new ContextualMenu(padding: 1, centered: true, selectedStyle: ContextualMenu.SelectedStyle.Dashes);
            _confirmRemoveMenu.AddMenuItem("Oui", RemoveSave);
            _confirmRemoveMenu.AddMenuItem("Annuler", LaunchThisScene);
        }

        public override void ExecuteScene()
        {
            List<Joueur> saves = _rpgContext.Joueur.Take(MAX_SAVES).ToList();
            string level;
            int menuY = 0;

            foreach (Joueur player in saves)
            {
                level = player.Niveau.ToString().PadLeft(2, '0').PadRight(3);
                DisplayTools.WriteInWindowAt(string.Format(_saveTemplate, level), SAVES_POS.X - 3, SAVES_POS.Y - 1 + (SAVES_GAP * menuY));
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

        private void TempAddNewPlayer()
        {
            DisplayTools.WriteInWindowAnimated(new string[]
            {
                "Vous vous apprétez à créer un nouveau personnage."
            });
            DisplayTools.WriteInWindowAnimated(new string[]
            {
                "Cependant, le développeur n'a pas encore implémenté cette",
                "possibilité..."
            });
            DisplayTools.WriteInWindowAnimated(new string[]
            {
                "HONTE À LUI !"
            });
            DisplayTools.WriteInWindowAnimated(new string[]
            {
                "Nan j'déconne, il fait de son mieux. Calme-toi."
            });

            LaunchThisScene();
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
            LaunchThisScene();
        }

        private void LaunchTitleMenuScene() => Game.ActiveScene = new TitleMenuScene();

        private void LaunchThisScene() => Game.ActiveScene = new GameSavesScene();

        private void LaunchCharacterCreationScene() => Game.ActiveScene = new CharacterCreationScene();

        #endregion
    }
}
