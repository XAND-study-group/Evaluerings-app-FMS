using MediatR;
using School.Application.Features.UserFeature.Login.Commands;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.UserEndpoints.Authentication;

public class RequestResetPassword : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Authentication:RequestResetPassword"] ??
                    throw new Exception("Route is not added to config file"),
                async (RequestResetPasswordRequest request, IMediator mediator)
                    => (await mediator.Send(new AccountRequestResetPasswordCommand(request))).ReturnHttpResult())
            .WithTags("Authentication")
            .RequireRateLimiting("LowFrequencyEndpoint");
    }
}