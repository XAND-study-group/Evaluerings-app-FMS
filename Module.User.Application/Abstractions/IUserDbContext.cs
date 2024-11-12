using Microsoft.EntityFrameworkCore;
using Module.User.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Abstractions
{
    public interface IUserDbContext
    {
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<AccountLogin> AccountLogins { get; set; }
        public DbSet<AccountClaim> AccountClaims { get; set; }
        public DbSet<Domain.Entities.User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
