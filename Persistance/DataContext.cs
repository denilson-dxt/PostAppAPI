using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}