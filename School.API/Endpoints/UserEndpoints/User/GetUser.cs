using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.User.Query;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.UserEndpoints.User
{
    public class GetUser : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:UserModule:User:GetUser"] ??
                       throw new Exception("Route is not added to config file"),
                    async (Guid userId, [FromServices] IMediator mediator) =>
                    await mediator.Send(new GetUserQuery(userId))).WithTags("User")
                .RequireAuthorization("User", "Teacher", "Admin");
        }
    }
}