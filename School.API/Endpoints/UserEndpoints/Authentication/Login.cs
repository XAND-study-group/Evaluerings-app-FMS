using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.Login.Commands;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.UserEndpoints.Authentication;

public class Login : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Authentication:Login"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] AuthenticateAccountLoginRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new AccountLoginCommand(request))).ReturnHttpResult()).WithTags("Authentication")
            .RequireRateLimiting("baseLimit");
    }
}