using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistense.EFCore.DTOs.DTOs;

namespace Persistense.EFCore.DTOs;

public class ApplicationContext : DbContext
{
    public DbSet<KeyValueRecord> KeyValueRecords { get; init; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        optionsBuilder.EnableDetailedErrors(true);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}