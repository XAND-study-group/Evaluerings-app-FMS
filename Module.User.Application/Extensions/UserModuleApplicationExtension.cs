using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Extensions
{
    public static class UserModuleApplicationExtension
    {
        public static IServiceCollection AddUserModuleApplication(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }
    }
}
