using Microsoft.EntityFrameworkCore;
using Module.Shared.Infrastructure.DbContexts;
using Module.User.Application.Abstractions;
using Module.User.Domain.Entities;

namespace Module.User.Infrastructure.DbContext
{
    public class UserDbContext : SchoolDbContext, IUserDbContext
    {
        public DbSet<Domain.Entities.Semester> Semesters { get; set; }
        public DbSet<AccountLogin> AccountLogins { get; set; }
        public DbSet<AccountClaim> AccountClaims { get; set; }
        public DbSet<Domain.Entities.User> Users { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options) 
        : base(options)
        {
        }
        protected override string ConnectionString { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User OnModelCreating

            modelBuilder.Entity<Domain.Entities.User>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Domain.Entities.User>()
                .Property(a => a.RowVersion)
                .IsRowVersion();

            #endregion

            #region Semester OnModelCreating

            modelBuilder.Entity<Semester>()
                .OwnsOne(p
                    => p.EducationRange);
            
            modelBuilder.Entity<Semester>()
                .OwnsOne(p
                    => p.SemesterNumber);
            
            modelBuilder.Entity<Semester>()
                .Property(a => a.RowVersion)
                .IsRowVersion();
            
            modelBuilder.Entity<Semester>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            #endregion


            #region Authentication OnModelCreating

            modelBuilder.Entity<AccountLogin>()
                .Property(a => a.RowVersion)
                .IsRowVersion(); // Configure RowVersion as a concurrency token

            // modelBuilder.Entity<AccountLogin>()
            //     .Ignore(a => a.Role);
            
            modelBuilder.Entity<AccountLogin>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
            
            modelBuilder.Entity<AccountClaim>()
                .Property(c => c.RowVersion)
                .IsRowVersion();
            
            modelBuilder.Entity<AccountClaim>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            #endregion
            
        }


    }
}
