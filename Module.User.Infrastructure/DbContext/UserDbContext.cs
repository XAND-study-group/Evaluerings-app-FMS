using Microsoft.EntityFrameworkCore;
using Module.Shared.Infrastructure.DbContexts;
using Module.User.Application.Abstractions;
using Module.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Infrastructure.DbContext
{
    public class UserDbContext : SchoolDbContext, IUserDbContext
    {
        public DbSet<Semester> Semesters { get; set; }
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

            modelBuilder.Entity<Semester>()
                .OwnsOne(p
                    => p.EducationRange);
            
            modelBuilder.Entity<Semester>()
                .OwnsOne(p
                    => p.SemesterNumber);

            #endregion

        }


    }
}
