using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.User.Application.Features.User.Query;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.User
{
    public class GetUsers : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app)
        {
            app.MapGet("/User", async ([FromServices] IMediator mediator)
                  => await mediator.Send(new GetUsersQuery())).WithTags("User")
                .RequireAuthorization();
        }
    }
}
