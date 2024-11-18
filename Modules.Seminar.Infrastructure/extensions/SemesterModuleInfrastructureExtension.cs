using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Semester.Application.Abstractions;
using Module.Semester.Infrastructure.DbContexts;
using Module.Semester.Infrastructure.Repositories;

namespace Module.Semester.Infrastructure.extensions;

public static class SemesterModuleInfrastructureExtension
{
    public static IServiceCollection AddSemesterModuleInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ISemesterDbContext, SemesterDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder =>
                {
                    optionsBuilder.MigrationsAssembly("Module.Semester.Infrastructure");
                    optionsBuilder.EnableRetryOnFailure();
                }));
        
        serviceCollection.AddScoped<IClassRepository, ClassRepository>();
        serviceCollection.AddScoped<ISemesterRepository, SemesterRepository>();
        serviceCollection.AddScoped<ILectureRepository, LectureRepository>();
        
        return serviceCollection;
    }
}