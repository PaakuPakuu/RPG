using System.Data.Entity;

namespace RPG
{
    public class RPGContext : DbContext
    {
        public RPGContext() : base("RPGContext")
        {

        }

        public DbSet<Ennemy> EnnemySet { get; set; }
    }
}
