using Microsoft.EntityFrameworkCore;

namespace DbService
{
    public partial class RpgContext : DbContext
    {
        private const string LOCAL_CONNECTION_STRING = "server=localhost;database=rpg_jh_local;port=3306;user id=root;password=root";
        private const string HEROKU_CONNECTION_STRING = "server=eu-cdbr-west-02.cleardb.net;database=heroku_b8db28dbd295a67;port=3306;user id=bd1a1c8389c3ab;password=fdd93476";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = HEROKU_CONNECTION_STRING;

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
