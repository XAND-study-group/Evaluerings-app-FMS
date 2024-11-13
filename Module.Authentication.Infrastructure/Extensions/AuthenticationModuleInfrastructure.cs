using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Module.Authentication.Infrastructure.Extensions;

public static class AuthenticationModuleInfrastructure
{
    public static IServiceCollection AddAuthenticationInfrastructureModule(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        return serviceCollection;
    }
}