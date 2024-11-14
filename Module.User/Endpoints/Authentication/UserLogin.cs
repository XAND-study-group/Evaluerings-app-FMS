using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Shared.Extensions;
using Module.User.Application.Features.Login.Commands;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.Authentication;

public class UserLogin : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("Authentication/Login",
            async ([FromBody] AuthenticateAccountLoginRequest request, [FromServices] IMediator mediator) =>
            (await mediator.Send(new AccountLoginCommand(request))).ReturnHttpResult()).WithTags("User");
    }
}