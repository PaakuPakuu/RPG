using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbService
{
    public partial class RpgContext : DbContext
    {
        private const string CONNECTION_STRING = "server=localhost;database=rpg_jh_local;port=3306;user id=root;password=root";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(CONNECTION_STRING, ServerVersion.AutoDetect(CONNECTION_STRING));
            }
        }
    }
}
