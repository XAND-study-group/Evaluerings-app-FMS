using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Infrastructure.DbContexts;
using Module.Feedback.Infrastructure.Repositories;

namespace Module.Feedback.Infrastructure.Extensions;

public static class FeedbackModuleInfrastructureExtension
{
    public static IServiceCollection AddFeedbackModuleInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<IFeedbackDbContext, FeedbackDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder =>
                {
                    optionsBuilder.MigrationsAssembly("Module.Semester.Infrastructure");
                    optionsBuilder.EnableRetryOnFailure();
                }));

        serviceCollection.AddScoped<IRoomRepository, RoomRepository>();
        
        return serviceCollection;
    }
}