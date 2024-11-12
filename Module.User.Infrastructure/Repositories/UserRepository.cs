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
        public async Task CreateUserAsync(Domain.Entities.User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Domain.Entities.User> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
