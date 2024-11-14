using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.User.Application.Features.User.Query;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.User
{
    public class GetUser : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app)
        {
            app.MapGet("/User/{userId:guid}",
                    async (Guid userId, [FromServices] IMediator mediator) =>
                    await mediator.Send(new GetUserQuery(userId))).WithTags("User")
                .RequireAuthorization("User", "Teacher", "Admin");
        }
    }
}