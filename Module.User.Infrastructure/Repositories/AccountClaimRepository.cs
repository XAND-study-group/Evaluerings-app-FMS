using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using Module.User.Application.Abstractions;
using Module.User.Domain.Entities;

namespace Module.User.Infrastructure.Repositories;

public class AccountClaimRepository(IUserDbContext dbContext) : IAccountClaimRepository
{
    async Task IAccountClaimRepository.CreateClaimForNewUserAsync(Domain.Entities.User user)
    {
        user.AddAccountClaim(AccountClaim.Create("Role", "User"));
        await dbContext.SaveChangesAsync();
    }

    async Task IAccountClaimRepository.AddClaimToUserAsync(AccountClaim claim)
    {
        await dbContext.SaveChangesAsync();
    }
}