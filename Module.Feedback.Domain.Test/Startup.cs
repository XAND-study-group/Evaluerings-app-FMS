using Microsoft.Extensions.DependencyInjection;
using Module.Feedback.Infrastructure.Mapper;

namespace Module.Feedback.Domain.Test;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfileFeedback));
    }
}