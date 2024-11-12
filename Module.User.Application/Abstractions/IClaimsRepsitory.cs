using Module.User.Domain.Entities;

namespace Module.User.Application.Abstractions;

public interface IClaimsRepsitory
{
    Task<IEnumerable<AccountClaim>> GetAccountClaimsFromIdAsync(Guid id);
    Task CreateClaimAsync(User.Domain.Entities.User user, AccountClaim claim);
}