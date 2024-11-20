using Microsoft.EntityFrameworkCore;
using Module.User.Application.Abstractions;
using Module.User.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext dbContext)
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