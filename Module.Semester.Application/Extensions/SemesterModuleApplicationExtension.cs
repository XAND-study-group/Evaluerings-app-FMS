using Microsoft.Extensions.DependencyInjection;

namespace Module.Semester.Application.Extensions;

public static class SemesterModuleApplicationExtension
{
    public static IServiceCollection AddSemesterModuleApplication(this IServiceCollection serviceCollection)
        => serviceCollection;
}