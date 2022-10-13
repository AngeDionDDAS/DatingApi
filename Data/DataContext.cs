using DatingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {

        }

        public DbSet<Value> values { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Photo> photos { get; set; }

    }
}
