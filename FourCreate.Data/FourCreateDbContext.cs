using FourCreate.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FourCreate.Data;
public class FourCreateDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SystemLog> SystemLogs { get; set; }

    public FourCreateDbContext(DbContextOptions<FourCreateDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>(x =>
        {
            x.HasMany(p => p.Employees).WithMany(p => p.Companies);
        });
    }
}
