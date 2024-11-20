using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Semester.Infrastructure.extensions;

namespace Module.Semester.Extensions;

public static class SemesterModuleExtension
{
    public static IServiceCollection AddSemesterModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSemesterModuleInfrastructure(configuration);
    }
}