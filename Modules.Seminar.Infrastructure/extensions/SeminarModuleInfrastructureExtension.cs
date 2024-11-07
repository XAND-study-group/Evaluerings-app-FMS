using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Seminar.Application.Abstractions;
using Module.Seminar.Infrastructure.DbContexts;
using Modules.Seminar.Infrastructure.Repositories;

namespace Modules.Seminar.Infrastructure.Extensions;

public static class SeminarModuleInfrastructureExtension
{
    public static IServiceCollection AddSeminarModuleInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<SeminarDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder =>
                {
                    optionsBuilder.MigrationsAssembly("Module.User.Infrastructure");
                    optionsBuilder.EnableRetryOnFailure();
                }));
        
        serviceCollection.AddScoped<ISeminarRepository, SeminarRepository>();
        
        return serviceCollection;
    }
}