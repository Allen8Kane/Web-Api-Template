using Microsoft.EntityFrameworkCore;

namespace ProjectOne
{
    public class ProjectContext : DbContext
    {
        public DbSet<User>? Users { get; set; }

        public string DbPath { get; private set; }

        public ProjectContext()
        {
            var CurrentDirectory = Directory.GetCurrentDirectory();
            DbPath = $"{CurrentDirectory}/db/test.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}