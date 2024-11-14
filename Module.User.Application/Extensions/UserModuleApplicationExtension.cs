using Microsoft.Extensions.DependencyInjection;
using Module.User.Domain.DomainServices;
using Module.User.Domain.DomainServices.Interfaces;

namespace Module.User.Application.Extensions;

public static class UserModuleApplicationExtension
{
    public static IServiceCollection AddUserModuleApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<ITokenProvider, TokenProvider>();
    }
}