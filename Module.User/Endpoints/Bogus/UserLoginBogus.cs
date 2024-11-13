using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Shared.Abstractions;
using Module.User.Application.Features.Bogus.Command;

namespace Module.User.Endpoints.Bogus;

public class UserLoginBogus : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("Bogus/GenerateUserLogins", async ([FromServices] IMediator mediator) => 
            await mediator.Send(new GenerateUserLoginsCommand())).WithTags("GenerateData");
    }
}