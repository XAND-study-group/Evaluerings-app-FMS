using Microsoft.EntityFrameworkCore;
using Module.Seminar.Domain.Entity;

namespace Module.Seminar.Infrastructure.DbContexts;

public class SeminarDbContext : DbContext
{
    public DbSet<Domain.Entity.Seminar> Seminars { get; set; }
    public DbSet<User> Users { get; set; }
    
    public SeminarDbContext(DbContextOptions<SeminarDbContext> options)
    :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Seminar OnModelCreating
        modelBuilder.Entity<Domain.Entity.Seminar>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Domain.Entity.Seminar>()
            .Property(s => s.RowVersion)
            .IsRowVersion();
        #endregion
    }
}