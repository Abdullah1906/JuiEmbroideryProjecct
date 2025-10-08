using JES.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DbSet<OrgNode> OrgNodes { get; set; }
        public DbSet<ShiftInfo> ShiftInfos { get; set; }
    }
 
}
