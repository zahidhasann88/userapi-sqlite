using Microsoft.EntityFrameworkCore;
using tasktest.Models;

namespace tasktest.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<userTest> userTest { get; set; }
    }
}
