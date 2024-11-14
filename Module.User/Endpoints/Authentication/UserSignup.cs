using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.User.Application.Features.SignUp.Commands;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.Authentication;

public class UserSignup : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("Authentication/SignUp",
            async ([FromBody] CreateAccountLoginRequest request, [FromServices] IMediator mediator) =>
            await mediator.Send(new AccountSignUpCommand(request))).WithTags("User")
            .RequireAuthorization();
    }
}