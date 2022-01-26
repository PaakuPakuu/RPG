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
        private RpgContext _rpgContext;

        private int _idPlayer;

        private Dice _dice4;
        private Dice _dice6;

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

            Console.ReadKey(true);

            Save();
            Game.ActiveScene = new TitleMenuScene();
        }

        private void Save()
        {
            using RpgContext rpgContext = new RpgContext();

            Joueur player = _rpgContext.Joueur.Single(j => j.IdJoueur == _idPlayer);

            player.Destin = _destin;
            player.Or = _fortune;
        }
    }
}
