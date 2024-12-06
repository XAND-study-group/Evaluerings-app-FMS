using Microsoft.EntityFrameworkCore;
using School.Domain.DomainServices.Interfaces;
using School.Domain.Entities;
using School.Infrastructure.DbContext;
using SharedKernel.Enums.Features.Authentication;

namespace School.Infrastructure.Repositories.User;

public class AccountClaimRepository(SchoolDbContext dbContext) : IAccountClaimRepository
{
    private const string RoleName = "Role";
    private const string PermissionName = "Permission";
    
    async Task IAccountClaimRepository.CreateClaimForRoleAsync(Domain.Entities.User user, Role role)
    {
        user.AddAccountClaim(AccountClaim.Create(RoleName, role.ToString()));
        
        switch (role)
        {
            case Role.User:
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "ReadFeedback"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "PostFeedback"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "CommentOnFeedback"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "AnswerExitSlip"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "VoteOnFeedback"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "ReadRoom"));
                break;
            
            case Role.Teacher:
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "ReadInteractedFeedback"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "ReadExitSlipAnswers"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "CreateExitSlips"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "PrintExitSlipReport"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "PrintFeedbackReport"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "CommentOnFeedback"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "RoomManagement"));
                user.AddAccountClaim(AccountClaim.Create(PermissionName, "ReadRoom"));
                break;  
            
            case Role.Admin:
                // TODO: Make admin
                break;
        }
        
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