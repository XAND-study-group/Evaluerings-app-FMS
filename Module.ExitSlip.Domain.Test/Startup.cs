using Microsoft.Extensions.DependencyInjection;
using Module.ExitSlip.Infrastructure.Mapper;

namespace Module.ExitSlip.Domain.Test;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfileExitSlip));
    }
}