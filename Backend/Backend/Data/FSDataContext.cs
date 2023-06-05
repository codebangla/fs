using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class FSDataContext : DbContext
    {
        public FSDataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> products { get; set; }
    }
}
