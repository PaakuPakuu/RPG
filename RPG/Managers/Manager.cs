using DbService;
using System.Collections.Generic;
using System.Linq;

namespace RPG
{
    public static class Manager
    {
        public static Player GetPlayer(DbService.Joueur joueur)
        {
            Player player = new Player()
            {
                Name = joueur.Nom,
                Position = new GeneralUtils.Point(joueur.PositionX, joueur.PositionY),
                Id = joueur.IdJoueur,
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
            };

            return player;
        }

        public static Map GetMap(DbService.Map map)
        {
            Map newMap = new Map(map.Filename)
            {
                Id = map.IdMap,
                SpawnPosition = new GeneralUtils.Point(map.PositionSpawnX, map.PositionSpawnY)
            };

            return newMap;
        }

        public static List<Ennemy> GetEnnemies()
        {
            //using RpgContext db = new RpgContext();
            //var e = db.GroupeMonstreJoueur.Where(g => g.IdJoueur == Game.Player.Id);
            //var a = db.GroupeMonstre.Where(g => g.);

            // Temporaire
            List<Ennemy> ennemies = new List<Ennemy>()
            {
                new Ennemy()
                {
                    Attack = 8,
                    Courage = 10,
                    GivenExperience = 20,
                    Health = 15,
                    MaxHealth = 15,
                    Impact = 7,
                    Name = "Gnôme",
                    Parade = 10
                },
                new Ennemy()
                {
                    Attack = 8,
                    Courage = 10,
                    GivenExperience = 15,
                    Health = 10,
                    MaxHealth = 10,
                    Impact = 5,
                    Name = "Petit gnôme",
                    Parade = 10
                },
                new Ennemy()
                {
                    Attack = 8,
                    Courage = 10,
                    GivenExperience = 15,
                    Health = 10,
                    MaxHealth = 10,
                    Impact = 5,
                    Name = "Petit gnôme",
                    Parade = 10
                }
            };

            return ennemies;
        }
    }
}
