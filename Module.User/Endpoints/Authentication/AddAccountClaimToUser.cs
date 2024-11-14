using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.User.Application.Features.AccountClaim.Command;
using SharedKernel.Dto.Features.Authentication.Command;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.Authentication;

public class AddAccountClaimToUser : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("Authentication/AddClaimToUser",
            async ([FromBody] AddClaimToUserRequest request, [FromServices] IMediator mediator) =>
            await mediator.Send(new AddClaimToUserCommand(request))).WithTags("User")
            .RequireAuthorization();
    }
}