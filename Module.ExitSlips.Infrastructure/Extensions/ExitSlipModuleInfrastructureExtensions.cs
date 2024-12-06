using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Infrastructure.DbContexts;
using Module.ExitSlip.Infrastructure.Mapper;
using Module.ExitSlip.Infrastructure.Repositories;
using SharedKernel.Interfaces.UOW;

namespace Module.ExitSlip.Infrastructure.Extensions;

public static class ExitSlipModuleInfrastructureExtensions
{
    public static IServiceCollection AddExitSlipModuleInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<IExitSlipDbContext, ExitSlipDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder => { optionsBuilder.MigrationsAssembly("Module.ExitSlip.Infrastructure"); }));

        serviceCollection.AddScoped<IExitSlipRepository, ExitSlipRepository>();
        serviceCollection.AddScoped<IQuestionRepository, QuestionRepository>();
        serviceCollection.AddScoped<IAnswerRepository, AnswerRepository>();

        serviceCollection.AddAutoMapper(typeof(MappingProfileExitSlip));
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<ExitSlipDbContext>>();
        return serviceCollection;
    }
}