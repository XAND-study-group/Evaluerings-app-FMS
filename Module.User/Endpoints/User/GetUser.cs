using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.User.Application.Features.User.Query;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.User
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