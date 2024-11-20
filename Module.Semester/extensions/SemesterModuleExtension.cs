using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Semester.Application.Extensions;
using Module.Semester.Infrastructure.extensions;

namespace Module.Semester.Extensions;

public static class SemesterModuleExtension
{
    public static IServiceCollection AddSemesterModule(this IServiceCollection serviceCollection, IConfiguration configuration)
        => serviceCollection
            .AddSemesterModuleApplication()
            .AddSemesterModuleInfrastructure(configuration);
}