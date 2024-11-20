﻿using School.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;

namespace School.Application.Abstractions.User;

public interface IAccountClaimRepository
{
    Task CreateClaimForRoleAsync(Domain.Entities.User user, Role role);
    Task AddClaimToUserAsync(AccountClaim claim);
    Task<AccountClaim> GetClaimByNameAsync(Domain.Entities.User user, string name);
}