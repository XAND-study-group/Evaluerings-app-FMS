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
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        async Task<Domain.Entities.User> IUserRepository.GetUserByIdAsync(Guid id)
            => await _dbContext.Users.SingleAsync(u => u.Id == id);
    }
}
