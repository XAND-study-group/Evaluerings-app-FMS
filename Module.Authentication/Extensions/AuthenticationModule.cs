using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Authentication.Application.Extensions;
using Module.Authentication.Infrastructure.Extensions;

namespace Module.Authentication.Extensions;

public static class AuthenticationModule
{
    public static IServiceCollection AddAuthenticationModule(this IServiceCollection serviceCollection,
        IConfiguration configuration)
        => serviceCollection
            .AddAuthenticationApplicationModule()
            .AddAuthenticationInfrastructureModule(configuration);
}