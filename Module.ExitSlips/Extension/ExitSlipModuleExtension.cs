using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.ExitSlip.Infrastructure.Extensions;

namespace Module.ExitSlip.Extension;

public static class ExitSlipModuleExtension
{
    public static IServiceCollection AddExitSlipModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExitSlipModuleInfrastructure(configuration);

        return services;
    }
}