using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<LibraryItem> libraryItems { get; set; }

        public DbSet<Api> Apis { get; set; }
    }
}
