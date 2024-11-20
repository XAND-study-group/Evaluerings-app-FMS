using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Application.Abstractions.Semester;
using School.Application.Abstractions.User;
using School.Infrastructure.DbContext;
using School.Infrastructure.Repositories.Semester;
using School.Infrastructure.Repositories.User;

namespace School.Infrastructure.Extensions
{
    public static class SchoolInfrastructureExtension
    {
        public static IServiceCollection AddSchoolInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<SchoolDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            optionsBuilder =>
            {   
                optionsBuilder.MigrationsAssembly("School.Infrastructure");
                optionsBuilder.EnableRetryOnFailure();
            }));

            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IAccountLoginRepository, AccountLoginRepository>();
            serviceCollection.AddScoped<IAccountClaimRepository, AccountClaimRepository>();
            
            serviceCollection.AddScoped<IClassRepository, ClassRepository>();
            serviceCollection.AddScoped<ISemesterRepository, SemesterRepository>();
            serviceCollection.AddScoped<ILectureRepository, LectureRepository>();

            return serviceCollection;
        }



      
    }
}
