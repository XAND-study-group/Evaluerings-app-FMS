using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.ExitSlip.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Extension
{
    public static class ExitSlipModuleExtension
    {
        public static IServiceCollection AddExitSlipModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExitSlipModuleInfrastructure(configuration);

            return services;
        }


    }
}
