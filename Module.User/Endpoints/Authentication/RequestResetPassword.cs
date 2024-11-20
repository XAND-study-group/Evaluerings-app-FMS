using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Module.User.Application.Features.Login.Commands;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.User.Endpoints.Authentication;

public class RequestResetPassword : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Authentication:RequestResetPassword"] ??
                    throw new Exception("Route is not added to config file"),
                async (RequestResetPasswordRequest request, IMediator mediator)
                    => (await mediator.Send(new AccountRequestResetPasswordCommand(request))).ReturnHttpResult())
            .WithTags("Authentication")
            .RequireRateLimiting("baseLimit");
    }
}