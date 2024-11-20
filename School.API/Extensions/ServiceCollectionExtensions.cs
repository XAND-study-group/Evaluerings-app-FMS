using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using School.Application.Extensions;
using School.Infrastructure.Extensions;

namespace School.API.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(o =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            };

            o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            };

            o.AddSecurityRequirement(securityRequirement);
        });

        return serviceCollection;
    }

    internal static IServiceCollection AddAuthorizationWithPolicies(this IServiceCollection serviceCollection)
        => serviceCollection.AddAuthorization(options =>
        {
            #region EvaluationApp policies
            
            options.AddPolicy("ReadFeedback", 
                policy => policy.RequireClaim("Permission", "ReadFeedback"));
            options.AddPolicy("ReadInteractedFeedback",
                policy => policy.RequireClaim("Permission", "ReadInteractedFeedback"));
            options.AddPolicy("PostFeedback", 
                policy => policy.RequireClaim("Permission", "PostFeedback"));
            options.AddPolicy("CommentOnFeedback", 
                policy => policy.RequireClaim("Permission", "CommentOnFeedback"));
            options.AddPolicy("AnswerExitSlip", 
                policy => policy.RequireClaim("Permission", "AnswerExitSlip"));
            options.AddPolicy("ReadExitSlipAnswers",
                policy => policy.RequireClaim("Permission", "ReadExistSlipAnswers"));
            options.AddPolicy("CreateExitSlips",
                policy => policy.RequireClaim("Permission", "CreateExistSlips"));
            options.AddPolicy("PrintExitSlipReport",
                policy => policy.RequireClaim("Permission", "PrintExitSlipReport"));
            options.AddPolicy("PrintFeedbackReport",
                policy => policy.RequireClaim("Permission", "PrintFeedbackReport"));

            #endregion

            #region School policies
            
            options.AddPolicy("Admin",
                policy => policy.RequireClaim("Role", "Admin"));
            options.AddPolicy("AdminOrTeacher",
                policy => policy.RequireClaim("Role", "Admin", "Teacher"));
            options.AddPolicy("Teacher",
                policy => policy.RequireRole("Role", "Teacher"));
            options.AddPolicy("TeacherOrUser",
                policy => policy.RequireRole("Role", "Teacher", "User"));
            options.AddPolicy("User",
                policy => policy.RequireRole("Role", "User"));

            #endregion
            
        });
    
    public static IServiceCollection AddSchool(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        return serviceCollection
            .AddSchoolApplication()
            .AddSchoolInfrastructure(configuration);
    }
}