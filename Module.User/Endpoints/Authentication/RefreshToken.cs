using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Module.User.Application.Features.Login.Commands;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.User.Endpoints.Authentication;

public class RefreshToken : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("Authentication/Refresh", async (TokenRequest request, IMediator mediator)
            => (await mediator.Send(new AccountRefreshTokenCommand(request))).ReturnHttpResult()).WithTags("User")
            .RequireRateLimiting("baseLimit");
    }
}