using Microsoft.Extensions.DependencyInjection;

namespace Module.Authentication.Application.Extensions;

public static class AuthenticationModuleApplication
{
    public static IServiceCollection AddAuthenticationApplicationModule(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}