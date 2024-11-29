using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.Login.Commands;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.UserEndpoints.Authentication;

public class ChangePassword : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Authentication:ChangePassword"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] ChangePasswordRequest request, [FromServices] IMediator mediator) =>
                await mediator.Send(new AccountChangePasswordCommand(request)))
            .WithTags("Authentication")
            .RequireAuthorization("AssureUserIsTheSame");
    }
}