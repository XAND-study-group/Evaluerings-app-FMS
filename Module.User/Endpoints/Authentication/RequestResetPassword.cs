using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Module.User.Application.Features.Login.Commands;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.User.Endpoints.Authentication;

public class RequestResetPassword : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("Authentication/RequestResetPassword", async (RequestResetPasswordRequest request, IMediator mediator)
            => (await mediator.Send(new AccountRequestResetPasswordCommand(request))).ReturnHttpResult()).WithTags("Authentication")
            .RequireRateLimiting("baseLimit");
    }
}