using Module.User.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;

namespace Module.User.Application.Abstractions;

public interface IAccountClaimRepository
{
    Task CreateClaimForRoleAsync(Domain.Entities.User user, Role role);
    Task AddClaimToUserAsync(AccountClaim claim);
    Task<AccountClaim> GetClaimByNameAsync(Domain.Entities.User user, string name);
}