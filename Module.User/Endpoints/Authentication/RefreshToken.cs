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

public class RefreshToken : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Authentication:RefreshToken"] ??
                    throw new Exception("Route is not added to config file"), async ([FromBody] TokenRequest request,
                    [FromServices] IMediator mediator)
                => (await mediator.Send(new AccountRefreshTokenCommand(request))).ReturnHttpResult())
            .WithTags("Authentication")
            .RequireRateLimiting("baseLimit");
    }
}