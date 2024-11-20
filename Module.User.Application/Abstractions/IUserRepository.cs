using SharedKernel.ValueObjects;

namespace Module.User.Application.Abstractions
{
    public interface IUserRepository
    {
        Task CreateUserAsync(Domain.Entities.User user);
        Task<Domain.Entities.User> GetUserByIdAsync(Guid id);
        Task<Domain.Entities.User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task SetUserRefreshTokenAsync(Domain.Entities.User user);
    }
}
