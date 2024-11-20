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
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<Subject> Subjects { get; set; }

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
            .HasMany(c => c.Teachers)
            .WithOne()
            .HasForeignKey("ClassIdTeacher");
        modelBuilder.Entity<Class>()
            .HasMany(c => c.Students)
            .WithOne()
            .HasForeignKey("ClassIdStudent");
        
        modelBuilder.Entity<Class>()
            .ComplexProperty<Text>(c => c.Description); 
        modelBuilder.Entity<Class>()
            .ComplexProperty<StudentCapacity>(c => c.StudentCapacity);

        #endregion
        
        #region Semester OnModelCreating
        modelBuilder.Entity<Domain.Entities.Semester>()
            .ToTable("Semesters")
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Domain.Entities.Semester>()
            .Property(c => c.RowVersion)
            .IsRowVersion();
        
        modelBuilder.Entity<Domain.Entities.Semester>()
            .ComplexProperty<EducationRange>(s => s.EducationRange);
        modelBuilder.Entity<Domain.Entities.Semester>()
            .ComplexProperty<SemesterNumber>(s => s.SemesterNumber);
        modelBuilder.Entity<Domain.Entities.Semester>()
            .ComplexProperty<SemesterName>(s => s.Name);

        #endregion
        
        #region Lecture OnModelCreating
        modelBuilder.Entity<Lecture>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Lecture>()
            .Property(c => c.RowVersion)
            .IsRowVersion();
        
        modelBuilder.Entity<Lecture>()
            .ComplexProperty<Title>(l => l.Title);
        modelBuilder.Entity<Lecture>()
            .ComplexProperty<Text>(l => l.Description);
        modelBuilder.Entity<Lecture>()
            .ComplexProperty<TimePeriod>(l => l.TimePeriod);
        modelBuilder.Entity<Lecture>()
            .ComplexProperty<LectureDate>(l => l.LectureDate);
        modelBuilder.Entity<Lecture>()
            .ComplexProperty(l => l.ClassRoom);

        #endregion Lecture OnModelCreating
    }
}