using Microsoft.EntityFrameworkCore;

namespace Evaluering.Module.Shared.Infrastructure
{
    public abstract class EvalueringDbContext : DbContext
    {
        protected abstract string ConnectionString { get; }

        protected EvalueringDbContext(DbContextOptions options) : base(options)
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
}
