using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Shared.Abstractions;
using Module.User.Application.Features.Authentication.Commands;
using SharedKernel.Dto.Features.Authentication.Command;

namespace Module.User.Endpoints.Authentication;

public class UserLogin : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("Authentication/Login",
            async ([FromBody] AuthenticateAccountLoginRequest request, [FromServices] IMediator mediator) =>
            await mediator.Send(new AccountLoginCommand(request))).WithTags("User")
            .RequireAuthorization();
    }
}