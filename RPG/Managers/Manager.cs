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

        public static Map GetMap(DbService.Map map)
        {
            Map newMap = new Map(map.Filename)
            {
                Id = map.IdMap,
                SpawnPosition = new GeneralUtils.Point(map.PositionSpawnX, map.PositionSpawnY)
            };

            return newMap;
        }
    }
}
