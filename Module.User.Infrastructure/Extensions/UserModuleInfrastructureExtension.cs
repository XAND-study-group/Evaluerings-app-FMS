using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.User.Application.Abstractions;
using Module.User.Infrastructure.DbContext;
using Module.User.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            return serviceCollection;
        }



      
    }
}
