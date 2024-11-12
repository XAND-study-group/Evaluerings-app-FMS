using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Abstractions;
using Module.Semester.Domain.Entities;
using Module.Shared.Infrastructure.DbContexts;
using SharedKernel.ValueObjects;

namespace Module.Semester.Infrastructure.DbContexts;

public class SemesterDbContext : SchoolDbContext, ISemesterDbContext
{
    public DbSet<Class> Classes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entities.Semester> Semesters { get; set; }
    
    public SemesterDbContext(DbContextOptions<SemesterDbContext> options)
    :base(options)
    {
    }

    protected override string ConnectionString { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Class OnModelCreating
        modelBuilder.Entity<Class>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Class>()
            .Property(c => c.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<Class>()
            .OwnsOne<Text>(c => c.Description); 
        modelBuilder.Entity<Class>()
            .OwnsOne<StudentCapacity>(c => c.StudentCapacity);

        #endregion
        
        #region Semester OnModelCreating
        modelBuilder.Entity<Domain.Entities.Semester>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Domain.Entities.Semester>()
            .Property(c => c.RowVersion)
            .IsRowVersion();
        
        modelBuilder.Entity<Domain.Entities.Semester>()
            .OwnsOne<EducationRange>(s => s.EducationRange);
        modelBuilder.Entity<Domain.Entities.Semester>()
            .OwnsOne<SemesterNumber>(s => s.SemesterNumber);

        #endregion
    }
}