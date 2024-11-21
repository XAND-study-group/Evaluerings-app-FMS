using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Feedback.Domain.DomainServices;
using Module.Feedback.Domain.DomainServices.Interfaces;

namespace Module.Feedback.Domain.Extensions;

public static class FeedbackModuleDomainExtension
{
    public static IServiceCollection AddFeedbackModuleDomain(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IHashIdService, HashIdService>();
        
        return serviceCollection;
    }
}