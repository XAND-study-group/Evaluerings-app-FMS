﻿using SharedKernel.ValueObjects;

namespace School.Application.Abstractions.User
{
    public interface IUserRepository
    {
        Task CreateUserAsync(Domain.Entities.User user);
        Task CreateUsersAsync(IEnumerable<Domain.Entities.User> users);
        Task<Domain.Entities.User?> GetUserByIdAsync(Guid id);
        Task<Domain.Entities.User?> GetUserByEmailAsync(string email);
        Task<Domain.Entities.User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task SetUserRefreshTokenAsync(Domain.Entities.User user);
        Task<IEnumerable<Domain.Entities.User>> GetAllUsers();
        Task ChangeUserPasswordAsync();
        Task<bool> DoesUserEmailExistAsync(string createRequestEmail);
    }
}
