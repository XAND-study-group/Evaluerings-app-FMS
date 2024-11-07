﻿using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Module.Shared.Domain.Abstractions;

namespace SportsRideKlubSkovly.API.Extensions
{
    public static class EndpointExtensions
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            var endpointServiceDescriptors = assembly
                .DefinedTypes.Where(type =>
                    type is { IsAbstract: false, IsInterface: false } &&
                    type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();
        
            services.TryAddEnumerable(endpointServiceDescriptors);
        
            return services;
        }
        
        public static IApplicationBuilder MapEndpoints(this WebApplication app)
        {
            var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
            foreach (var endpoint in endpoints)
            {
                endpoint.MapEndpoint(app);
            }
        
            return app;
        }
    }
}
