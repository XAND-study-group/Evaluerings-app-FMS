using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Application.Services;
using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Infrastructure.DbContexts;
using Module.Feedback.Infrastructure.Mapper;
using Module.Feedback.Infrastructure.Proxy;
using Module.Feedback.Infrastructure.Repositories;
using SharedKernel.Interfaces.UOW;

namespace Module.Feedback.Infrastructure.Extensions;

public static class FeedbackModuleInfrastructureExtension
{
    public static IServiceCollection AddFeedbackModuleInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<IFeedbackDbContext, FeedbackDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder => { optionsBuilder.MigrationsAssembly("Module.Feedback.Infrastructure"); }));

        serviceCollection.AddHttpClient<IValidationServiceProxy, ValidationServiceProxy>();

        serviceCollection.AddScoped<IRoomRepository, RoomRepository>();
        serviceCollection.AddScoped<IFeedbackRepository, FeedbackRepository>();
        serviceCollection.AddScoped<ICommentRepository, CommentRepository>();
        serviceCollection.AddScoped<IValidationServiceProxy, ValidationServiceProxy>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<FeedbackDbContext>>();
        serviceCollection.AddScoped<IVoteRepository, VoteRepository>();
        serviceCollection.AddScoped<IEmailNotificationProxy, EmailNotificationProxy>();
        serviceCollection.AddScoped<ISchoolApiProxy, SchoolApiProxy>();

        serviceCollection.AddAutoMapper(typeof(MappingProfileFeedback));

        return serviceCollection;
    }
}