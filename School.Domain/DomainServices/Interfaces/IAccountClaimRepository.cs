using School.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;

namespace School.Domain.DomainServices.Interfaces;

public interface IAccountClaimRepository
{
    Task CreateClaimForRoleAsync(User user, Role role);
    Task AddClaimToUserAsync(AccountClaim claim);
    Task<AccountClaim> GetClaimByNameAsync(User user, string name);
}