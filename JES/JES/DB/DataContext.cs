using JES.Models;
using Microsoft.EntityFrameworkCore;

namespace JES.DB
{
    public class DataContext:DbContext
    {
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options)
         : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
 
}
