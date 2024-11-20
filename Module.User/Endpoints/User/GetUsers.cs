using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.User.Application.Features.User.Query;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.User
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