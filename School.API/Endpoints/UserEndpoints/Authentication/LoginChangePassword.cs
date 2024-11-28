using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.Login.Commands;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.UserEndpoints.Authentication;

public class LoginChangePassword : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Authentication:LoginChangePassword"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] ResetPasswordRequest request, [FromServices] IMediator mediator) =>
                await mediator.Send(new AccountResetPasswordCommand(request)))
            .WithTags("Authentication")
            .RequireRateLimiting("LowFrequencyEndpoint");
    }
}