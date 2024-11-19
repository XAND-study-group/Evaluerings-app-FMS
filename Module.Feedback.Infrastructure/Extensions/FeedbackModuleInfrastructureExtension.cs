using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.DomainServices;
using Module.Feedback.Infrastructure.DbContexts;
using Module.Feedback.Infrastructure.Proxy;
using Module.Feedback.Infrastructure.Repositories;

namespace Module.Feedback.Infrastructure.Extensions;

public static class FeedbackModuleInfrastructureExtension
{
    public static IServiceCollection AddFeedbackModuleInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<IFeedbackDbContext, FeedbackDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder =>
                {
                    optionsBuilder.MigrationsAssembly("Module.Semester.Infrastructure");
                    optionsBuilder.EnableRetryOnFailure();
                }));

        serviceCollection.AddHttpClient<IValidationServiceProxy, ValidationServiceProxy>(client =>
        {
            client.BaseAddress = new Uri(configuration["GeminiApiURL"] ?? string.Empty);
        });

        serviceCollection.AddScoped<IRoomRepository, RoomRepository>();
        serviceCollection.AddScoped<IFeedbackRepository, FeedbackRepository>();
        serviceCollection.AddScoped<ICommentRepository, CommentRepository>();
        serviceCollection.AddScoped<IValidationServiceProxy, ValidationServiceProxy>();

        return serviceCollection;
    }
}