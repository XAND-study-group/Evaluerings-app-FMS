using Microsoft.Extensions.DependencyInjection;

namespace Module.Feedback.Domain.Extensions;

public static class FeedbackModuleDomainExtension
{
    public static IServiceCollection AddFeedbackModuleDomain(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}