using DbService;
using RPG;

namespace DataTransferManager
{
    public class PlayerManager
    {
        public Player GetPlayer(Joueur joueur)
        {
            Player player = new Player()
            {
                Name = joueur.Nom,
                Position = new GeneralUtils.Point(),
                Stats = new HeroStats()
                {
                    Attack = joueur.Attaque,
                    Parade = joueur.Parade,
                    Courage = joueur.Courage,
                    Intelligence = joueur.Intelligence,
                    Charism = joueur.Charisme,
                    Dexterity = joueur.Adresse,
                    Strength = joueur.Force,
                    AstralEnergy = joueur.EnergieAstrale,
                    Destin = joueur.Destin,
                    Experience = joueur.Experience,
                    Level = joueur.Niveau,
                    Gold = joueur.Or,
                    Silver = joueur.Argent,
                    Health = joueur.PointsVie
                }
            };

            return player;
        }
    }
}
