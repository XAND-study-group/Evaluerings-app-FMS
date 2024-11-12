using Microsoft.EntityFrameworkCore;

namespace Module.Shared.Infrastructure.DbContexts;

public abstract class SchoolDbContext : DbContext
{
    protected abstract string ConnectionString { get; }

    protected SchoolDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!string.IsNullOrEmpty(ConnectionString))
            modelBuilder.HasDefaultSchema(ConnectionString);
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(true, cancellationToken);
    }
}