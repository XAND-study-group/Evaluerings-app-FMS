﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.User.Application.Features.Login.Commands;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.Authentication;

public class LoginChangePassword : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Authentication:LoginChangePassword"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] ChangePasswordRequest request, [FromServices] IMediator mediator) =>
                await mediator.Send(new AccountChangePasswordCommand(request))).WithTags("Authentication")
            .RequireRateLimiting("baseLimit");
    }
}