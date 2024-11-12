﻿using Module.Authentication.Domain.Entity;

namespace Module.Authentication.Application.Abstractions.Repositories;

public interface IClaimsRepsitory
{
    Task<IEnumerable<AccountClaim>> GetAccountClaimsFromIdAsync(Guid id);
    Task CreateClaimAsync(User user, AccountClaim claim);
}