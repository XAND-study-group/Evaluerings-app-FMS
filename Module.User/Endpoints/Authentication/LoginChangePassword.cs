﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.User.Application.Features.Login.Commands;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.Authentication;

public class LoginChangePassword : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("Authentication/ChangePassword", async ([FromBody] ChangePasswordRequest request, [FromServices] IMediator mediator) =>
                await mediator.Send(new AccountChangePasswordCommand(request))).WithTags("User")
                .RequireAuthorization();
    }
}