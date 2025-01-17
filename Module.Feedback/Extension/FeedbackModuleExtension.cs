﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Feedback.Infrastructure.Extensions;

namespace Module.Feedback.Extension;

public static class FeedbackModuleExtension
{
    public static IServiceCollection AddFeedbackModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFeedbackModuleInfrastructure(configuration);

        return services;
    }
}