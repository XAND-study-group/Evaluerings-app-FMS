using Microsoft.EntityFrameworkCore;
using Module.User.Application.Abstractions;
using Module.User.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;

namespace Module.User.Infrastructure.Repositories;

public class AccountLoginRepository(IUserDbContext dbContext, IAccountClaimRepository accountClaimRepository)
    : IAccountLoginRepository
{
    async Task<AccountLogin> IAccountLoginRepository.GetAccountLoginFromIdAsync(Guid id)
    {
        return await dbContext.AccountLogins.FirstOrDefaultAsync(account => account.Id == id) ??
               throw new ArgumentException("Login blev ikke fundet");
    }

    async Task<AccountLogin?> IAccountLoginRepository.GetAccountLoginFromEmailAsync(string email)
    {
        return await dbContext.AccountLogins
                   .Include(account => account.User)
                   .ThenInclude(user => user.AccountClaims)
                   .FirstOrDefaultAsync(login => login.Email == email) ??
               throw new Exception("Login blev ikke fundet");
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

    async Task IAccountLoginRepository.ChangeLoginPasswordAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}