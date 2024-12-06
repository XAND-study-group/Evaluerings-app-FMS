using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Application.Abstractions.Semester;
using School.Application.Abstractions.User;
using School.Domain.DomainServices.Interfaces;
using School.Infrastructure.DbContext;
using School.Infrastructure.Mapper;
using School.Infrastructure.Repositories.Semester;
using School.Infrastructure.Repositories.User;
using School.Infrastructure.Services;
using SharedKernel.Interfaces.UOW;

namespace School.Infrastructure.Extensions;

public static class SchoolInfrastructureExtension
{
    public static IServiceCollection AddSchoolInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<SchoolDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                optionsBuilder => { optionsBuilder.MigrationsAssembly("School.Infrastructure"); }));

        serviceCollection.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAccountClaimRepository, AccountClaimRepository>()
            .AddScoped<IClassRepository, ClassRepository>()
            .AddScoped<ISemesterRepository, SemesterRepository>()
            .AddScoped<ILectureRepository, LectureRepository>()
            .AddScoped<ISubjectRepository, SubjectRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork<SchoolDbContext>>()
            .AddScoped<IUserDomainService, UserDomainService>();

        serviceCollection.AddAutoMapper(typeof(MappingProfileSchool));

        return serviceCollection;
    }
}