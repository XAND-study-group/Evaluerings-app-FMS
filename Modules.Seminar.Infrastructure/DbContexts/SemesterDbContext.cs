using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Abstractions;
using Module.Semester.Domain.Entity;
using Module.Shared.Infrastructure.DbContexts;

namespace Module.Semester.Infrastructure.DbContexts;

public class SemesterDbContext : SchoolDbContext, ISemesterDbContext
{
    public DbSet<Class> Classes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entity.Semester> Semesters { get; set; }
    
    public SemesterDbContext(DbContextOptions<SemesterDbContext> options)
    :base(options)
    {
    }

    protected override string ConnectionString { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Class OnModelCreating
        modelBuilder.Entity<Semester.Domain.Entity.Class>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Semester.Domain.Entity.Class>()
            .Property(s => s.RowVersion)
            .IsRowVersion();
        #endregion
    }
}