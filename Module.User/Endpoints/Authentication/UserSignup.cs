﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Shared.Abstractions;
using Module.User.Application.Features.SignUp.Commands;
using SharedKernel.Dto.Features.Authentication.Command;

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