using Microsoft.AspNetCore.Builder;
using Module.Shared.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Module.User.Application.Features.User.Query;

namespace Module.User.Endpoints.UserManagement
{
    public class GetUsers : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app)
        {
            app.MapGet("User", async ([FromServices] IMediator mediator)
                  => await mediator.Send(new GetUsersQuery()));
        }
    }
}
