using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.User.Application.Abstractions;
using Module.User.Infrastructure.DbContext;
using Module.User.Infrastructure.Repositories;

namespace Module.User.Infrastructure.Extensions
{
    public static class UserModuleInfrastructureExtension
    {
        public static IServiceCollection AddUserModuleInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<IUserDbContext, UserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            optionsBuilder =>
            {   
                optionsBuilder.MigrationsAssembly("Module.User.Infrastructure");
                optionsBuilder.EnableRetryOnFailure();
            }));

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IAccountLoginRepository, AccountLoginRepository>();
            serviceCollection.AddScoped<IAccountClaimRepository, AccountClaimRepository>();

            return serviceCollection;
        }



      
    }
}
