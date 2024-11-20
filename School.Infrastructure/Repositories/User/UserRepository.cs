using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.User;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolDbContext _dbContext;
        public UserRepository(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        async Task IUserRepository.CreateUserAsync(Domain.Entities.User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        async Task<IEnumerable<Domain.Entities.User>> IUserRepository.GetAllUsers()
            => await _dbContext.Users.ToListAsync();

        async Task<Domain.Entities.User> IUserRepository.GetUserByIdAsync(Guid id)
            => await _dbContext.Users.SingleAsync(u => u.Id == id);

        async Task<Domain.Entities.User?> IUserRepository.GetUserByRefreshTokenAsync(string refreshToken)
            => await _dbContext.Users.Include(user => user.AccountClaims).FirstOrDefaultAsync(user => user.RefreshToken.Token == refreshToken);

        async Task IUserRepository.SetUserRefreshTokenAsync(Domain.Entities.User user)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}