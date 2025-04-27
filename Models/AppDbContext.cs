using Microsoft.EntityFrameworkCore;

namespace safriltest2_entitydata.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<TestingNameModel> TestingName { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
