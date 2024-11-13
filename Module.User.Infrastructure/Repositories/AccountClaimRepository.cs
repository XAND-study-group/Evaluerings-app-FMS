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

    async Task<AccountClaim> IAccountClaimRepository.GetClaimByNameAsync(Domain.Entities.User user, string claimName)
    {
        var foundUser = dbContext.Users.Include(u => u.AccountClaims)
                            .Where(u => u.Id == user.Id) ??
                        throw new ArgumentException("Bruger kunne ikk findes");

        return await foundUser.SelectMany(u => u.AccountClaims)
                   .FirstOrDefaultAsync(claim => claim.ClaimName == claimName) ??
               throw new ArgumentException("Claim kunne ikke findes");
    }
}