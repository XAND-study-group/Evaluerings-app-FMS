using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.AccountClaim.Command;
using SharedKernel.Dto.Features.School.Authentication.Command;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.UserEndpoints.Authentication;

public class AddAccountClaimToUser : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(
                configuration["Routes:UserModule:Authentication:AddAccountClaimToUser"] ??
                throw new Exception("Route is not added to config file"),
                async ([FromBody] AddClaimToUserRequest request, [FromServices] IMediator mediator) =>
                await mediator.Send(new AddClaimToUserCommand(request))).WithTags("Authentication")
            .RequireAuthorization();
    }
}