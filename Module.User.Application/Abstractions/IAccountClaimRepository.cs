﻿using Module.User.Domain.Entities;

namespace Module.User.Application.Abstractions;

public interface IAccountClaimRepository
{
    Task CreateClaimForNewUserAsync(Domain.Entities.User user);
    Task AddClaimToUserAsync(AccountClaim claim);
    Task<AccountClaim> GetClaimByNameAsync(Domain.Entities.User user, string name);
}