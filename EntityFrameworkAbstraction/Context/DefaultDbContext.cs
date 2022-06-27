using EntityFrameworkAbstraction.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkAbstraction.Context;

public class DefaultDbContext : DbContext
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base (options)
    {
    }

    public DbSet<User> Users { get; set; }
}