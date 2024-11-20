using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.SignUp.Commands;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.UserEndpoints.Authentication;

public class Signup : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Authentication:Signup"] ??
                    throw new Exception("Route is not added to config file"),
            async ([FromBody] CreateAccountLoginRequest request, [FromServices] IMediator mediator) =>
            await mediator.Send(new AccountSignUpCommand(request))).WithTags("Authentication")
            .RequireAuthorization();
    }
}