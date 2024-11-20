using Microsoft.EntityFrameworkCore;
using School.Application.Abstractions.User;
using School.Domain.Entities;
using School.Infrastructure.DbContext;

namespace School.Infrastructure.Repositories.User;

public class AccountLoginRepository(SchoolDbContext dbContext, IAccountClaimRepository accountClaimRepository)
    : IAccountLoginRepository
{
    async Task<AccountLogin?> IAccountLoginRepository.GetAccountLoginFromIdAsync(Guid id)
    {
        return await dbContext.AccountLogins.FirstOrDefaultAsync(account => account.Id == id);
    }

    async Task<AccountLogin?> IAccountLoginRepository.GetAccountLoginFromEmailAsync(string email)
    {
        return await dbContext.AccountLogins
            .Include(account => account.User)
            .ThenInclude(user => user.AccountClaims)
            .FirstOrDefaultAsync(login => login.Email == email);
    }

    async Task IAccountLoginRepository.CreateAccountLoginAsync(AccountLogin accountLogin)
    {
        await accountClaimRepository.CreateClaimForRoleAsync(accountLogin.User, accountLogin.Role);
        await dbContext.AccountLogins.AddAsync(accountLogin);
        await dbContext.SaveChangesAsync();
    }

    async Task IAccountLoginRepository.CreateAccountLoginsAsync(IEnumerable<AccountLogin> accountLogins)
    {
        var accountList = accountLogins.ToList();
        foreach (var accountLogin in accountList)
            await accountClaimRepository.CreateClaimForRoleAsync(accountLogin.User, accountLogin.Role);
        
        await dbContext.AccountLogins.AddRangeAsync(accountList);
        await dbContext.SaveChangesAsync();
    }

    async Task<bool> IAccountLoginRepository.DoesAccountLoginEmailExistAsync(string email)
    {
        return await dbContext.AccountLogins.AnyAsync(login => login.Email == email);
    }

    async Task IAccountLoginRepository.ChangeLoginPasswordAsync(AccountLogin accountLogin)
    {
        await dbContext.SaveChangesAsync();
    }
}