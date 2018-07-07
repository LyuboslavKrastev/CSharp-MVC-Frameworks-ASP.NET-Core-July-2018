using FDMC.Models;
using Microsoft.EntityFrameworkCore;

namespace FDMC.Data
{
    public class CatAppContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SqlServerConnectionString.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
