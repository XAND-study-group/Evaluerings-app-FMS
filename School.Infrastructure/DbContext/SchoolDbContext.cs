using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace School.Infrastructure.DbContext
{
    public class SchoolDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<AccountClaim> AccountClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) 
        : base(options)
        {
        }
        
        protected string ConnectionString { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User OnModelCreating

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .OwnsMany(u
                    => u.RefreshTokens);
            
            modelBuilder.Entity<User>()
                .ComplexProperty(u
                    => u.PasswordHash);

            modelBuilder.Entity<User>()
                .Property(u => u.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<User>()
                .ComplexProperty<UserFirstname>(u => u.Firstname);
            modelBuilder.Entity<User>()
                .ComplexProperty<UserLastname>(u => u.Lastname);
            modelBuilder.Entity<User>()
                .ComplexProperty<UserEmail>(u => u.Email);
            
            // modelBuilder.Entity<User>()
            //     .Ignore(a => a.Role);

            #endregion
            
            #region Authentication OnModelCreating
            
            modelBuilder.Entity<AccountClaim>()
                .Property(c => c.RowVersion)
                .IsRowVersion();
            
            modelBuilder.Entity<AccountClaim>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            #endregion
            
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
            modelBuilder.Entity<Semester>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Semester>()
                .Property(c => c.RowVersion)
                .IsRowVersion();
            
            modelBuilder.Entity<Semester>()
                .ComplexProperty<EducationRange>(s => s.EducationRange);
            modelBuilder.Entity<Semester>()
                .ComplexProperty<SemesterNumber>(s => s.SemesterNumber);
            modelBuilder.Entity<Semester>()
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

            #region Subject OnModelCreating

            modelBuilder.Entity<Subject>()
                .Property(s => s.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<Subject>()
                .ComplexProperty<SubjectDescription>(s => s.Description);
            
            modelBuilder.Entity<Subject>()
                .ComplexProperty<SubjectName>(s => s.Name);

            #endregion
            
            if (!string.IsNullOrEmpty(ConnectionString))
                modelBuilder.HasDefaultSchema(ConnectionString);
        
            base.OnModelCreating(modelBuilder);
        
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }


    }
}
