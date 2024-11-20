using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.User.Query;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.UserEndpoints.User
{
    public class GetUsers : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:UserModule:User:GetUsers"] ??
                       throw new Exception("Route is not added to config file"),
                    async ([FromServices] IMediator mediator)
                        => await mediator.Send(new GetUsersQuery())).WithTags("User")
                .RequireAuthorization("AdminOrTeacher");
        }
    }
}