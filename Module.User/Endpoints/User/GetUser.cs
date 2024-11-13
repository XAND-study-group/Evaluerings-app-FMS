using Microsoft.AspNetCore.Builder;
using Module.Shared.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Module.User.Application.Features.User.Query;

namespace Module.User.Endpoints.User
{
    public class GetUser : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app)
        {
            app.MapGet("/User/{userId:guid}",
                    async (Guid userId, [FromServices] IMediator mediator) =>
                    await mediator.Send(new GetUserQuery(userId))).WithTags("User")
                .RequireAuthorization();
        }
    }
}