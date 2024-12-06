using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.User;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.User;

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
    {
        return await dbContext.Users.ToListAsync();
    }

    async Task<Domain.Entities.User?> IUserRepository.GetUserByIdAsync(Guid id)
    {
        return await dbContext.Users
            .Include(user => user.RefreshTokens)
            .Include(user => user.AccountClaims)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    async Task<Domain.Entities.User?> IUserRepository.GetUserByEmailAsync(string email)
    {
        return await dbContext.Users
            .Include(user => user.AccountClaims)
            .FirstOrDefaultAsync(user => user.Email.Value == email);
    }

    async Task<Domain.Entities.User?> IUserRepository.GetUserByRefreshTokenAsync(string refreshToken)
    {
        return await dbContext.Users
            .Include(user => user.AccountClaims)
            .FirstOrDefaultAsync(user => user.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    async Task IUserRepository.AddUserRefreshTokenAsync(Domain.Entities.User user)
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
    {
        return await dbContext.Users.AnyAsync(user => user.Email.Value == createRequestEmail);
    }

    public async Task SignOutUserAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}