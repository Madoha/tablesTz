using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using tablestz_api.Models;

namespace tablestz_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<SavedWord> SavedWords { get; set; }
    }
}
