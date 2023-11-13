using EntityFramework.Exceptions.MySQL;
using FourCreate.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity.Infrastructure;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>(x =>
        {
            x.HasMany(p => p.Employees).WithMany(p => p.Companies);
            x.HasIndex(p => p.Name).IsUnique();
        });

        modelBuilder.Entity<Employee>(x =>
        {
            x.HasIndex(p => p.Email).IsUnique();
        });
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        trackable.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        break;
                }
            }
        }
    }
}
