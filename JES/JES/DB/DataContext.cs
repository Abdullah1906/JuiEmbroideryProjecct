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
    }
 
}
