using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.User.Application.Features.Bogus.Command;
using SharedKernel.Interfaces;

namespace Module.User.Endpoints.Bogus;

public class GenerateUserLoginBogus : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Bogus:GenerateUserLoginBogus"] ??
                    throw new Exception("Route is not added to config file"),
            async ([FromServices] IMediator mediator) =>
            await mediator.Send(new GenerateUserLoginsCommand())).WithTags("GenerateData");
    }
}