using Microsoft.EntityFrameworkCore;
using Module.User.Application.Abstractions;
using Module.User.Domain.Entities;

namespace Module.User.Infrastructure.Repositories;

public class AccountLoginRepository(IUserDbContext dbContext) : IAccountLoginRepository
{
    async Task<AccountLogin?> IAccountLoginRepository.GetAccountLoginFromEmailAsync(string email)
    {
        return await dbContext.AccountLogins.FirstOrDefaultAsync(login => login.Email == email) ?? throw new Exception("Account login was not found");
    }

    async Task IAccountLoginRepository.CreateAccountLoginAsync(AccountLogin accountLogin)
    {
        await dbContext.AccountLogins.AddAsync(accountLogin);
        await dbContext.SaveChangesAsync();
    }

    async Task<bool> IAccountLoginRepository.DoesAccountLoginEmailExistAsync(string email)
    {
        return await dbContext.AccountLogins.AnyAsync(login => login.Email == email);
    }
}