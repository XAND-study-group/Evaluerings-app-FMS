using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.User.Application.Features.Login.Commands;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.User.Endpoints.Authentication;

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