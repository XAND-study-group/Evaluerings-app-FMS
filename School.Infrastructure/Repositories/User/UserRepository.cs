using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.User;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.User
{
    public class UserRepository(SchoolDbContext dbContext) : IUserRepository
    {
        async Task IUserRepository.CreateUserAsync(Domain.Entities.User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        async Task IUserRepository.CreateUsersAsync(IEnumerable<Domain.Entities.User> users)
        {
            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }

        async Task<IEnumerable<Domain.Entities.User>> IUserRepository.GetAllUsers()
            => await dbContext.Users.ToListAsync();

        async Task<Domain.Entities.User?> IUserRepository.GetUserByIdAsync(Guid id)
            => await dbContext.Users.SingleAsync(u => u.Id == id);

        async Task<Domain.Entities.User?> IUserRepository.GetUserByEmailAsync(string email)
            => await dbContext.Users.Include(user => user.AccountClaims).FirstOrDefaultAsync(user => user.Email.Value == email);

        async Task<Domain.Entities.User?> IUserRepository.GetUserByRefreshTokenAsync(string refreshToken)
            => await dbContext.Users.Include(user => user.AccountClaims)
                .FirstOrDefaultAsync(user => user.RefreshToken.Token == refreshToken);

        async Task IUserRepository.SetUserRefreshTokenAsync(Domain.Entities.User user)
        {
            await dbContext.SaveChangesAsync();
        }

        async Task IUserRepository.ChangeUserPasswordAsync(Domain.Entities.User user, byte[] rowVersion)
        {
            user.ResetRefreshToken();
            dbContext.Entry(user).Property(nameof(user.RowVersion)).OriginalValue = rowVersion;
            await dbContext.SaveChangesAsync();
        }

        async Task<bool> IUserRepository.DoesUserEmailExistAsync(string createRequestEmail)
            => await dbContext.Users.AnyAsync(user => user.Email.Value == createRequestEmail);
    }
}