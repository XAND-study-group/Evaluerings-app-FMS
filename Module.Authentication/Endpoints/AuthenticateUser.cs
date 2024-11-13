using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Authentication.Application.Features.Authentication.Commands;
using Module.Authentication.Domain.Entity;
using Module.Shared.Abstractions;

namespace Module.Authentication.Endpoints;

public class AuthenticateUser : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        // app.MapPost("Authentication/Login", async ([FromBody] AccountLogin login, [FromServices] IMediator mediator) =>
        // await mediator.Send(new AccountLoginCommand()));
    }
}