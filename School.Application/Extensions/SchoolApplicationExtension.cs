using Microsoft.Extensions.DependencyInjection;
using School.Domain.DomainServices;
using School.Domain.DomainServices.Interfaces;

namespace School.Application.Extensions;

public static class SchoolApplicationExtension
{
    public static IServiceCollection AddSchoolApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ITokenProvider, TokenProvider>();
    }
}