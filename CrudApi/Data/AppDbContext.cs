using CrudApi.Students;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Data Source=Data.sqlite");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
