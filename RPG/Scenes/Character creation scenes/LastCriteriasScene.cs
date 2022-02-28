using DbService;
using GeneralUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG
{
    public class LastCriteriasScene : DefaultScene
    {
        private readonly RpgContext _rpgContext;

        private readonly int _idPlayer;

        private readonly Dice _dice4;
        private readonly Dice _dice6;

        private int _destin;
        private int _fortune;

        public LastCriteriasScene(int idPlayer)
        {
            _rpgContext = new RpgContext();

            _idPlayer = idPlayer;

            _dice4 = new Dice(4);
            _dice6 = new Dice(6);
        }

        public override void ExecuteScene()
        {
            DisplayTools.WriteInWindowCenter("Dernière étape !", y: 2, animated: true);
            DisplayTools.WriteInWindowCenter("★ Points DESTIN ★", y: 5);

            _destin = _dice4.RollWithAnimation(y: 6) - 1;
            DisplayTools.WriteInWindowCenter("- 1", 44, 7, animated: true);

            DisplayTools.WriteInWindowCenter("◉◉ Fortune de départ ◉◉", y: 9);

            int fortune1 = _dice6.RollWithAnimation(25, 10);
            int fortune2 = _dice6.RollWithAnimation(47, 10);

            _fortune = (fortune1 + fortune2) * 10;

            DisplayTools.WriteInWindowCenter($"×10 → {_fortune} ◉", y: 11, animated: true);
            DisplayTools.WriteInWindowCenter("• Press [ENTER] •", y: ScreenHeight - 2);

            Console.ReadKey(true);

            DisplayTools.WriteInDialogBox(new string[]
            {
                "Votre personnage a été créé avec succés !",
                "\nPréparez-vous à l'aventure."
            });

            Save();
            Game.ActiveScene = new GameScene();
        }

        private void Save()
        {
            Joueur player = _rpgContext.Joueur.Single(j => j.IdJoueur == _idPlayer);

            player.Destin = _destin;
            player.Or = _fortune;

            _rpgContext.SaveChanges();

            Game.Player = Manager.GetPlayer(player);
            Game.CurrentMap = Manager.GetMap(_rpgContext.Map.Single(m => m.IdMap == player.IdMapCourante));

            Game.Player.Position.X = Game.CurrentMap.SpawnPosition.X;
            Game.Player.Position.Y = Game.CurrentMap.SpawnPosition.Y;
        }
    }
}
