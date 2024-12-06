using Microsoft.Extensions.DependencyInjection;
using School.Infrastructure.Mapper;

namespace School.Domain.Test.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfileSchool));
    }
}