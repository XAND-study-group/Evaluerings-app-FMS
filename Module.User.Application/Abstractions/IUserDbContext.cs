using Microsoft.EntityFrameworkCore;
using Module.User.Domain.Entities;

namespace Module.User.Application.Abstractions
{
    public interface IUserDbContext
    {
        public DbSet<Domain.Entities.Semester> Semesters { get; set; }
        public DbSet<AccountLogin> AccountLogins { get; set; }
        public DbSet<AccountClaim> AccountClaims { get; set; }
        public DbSet<Domain.Entities.User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
