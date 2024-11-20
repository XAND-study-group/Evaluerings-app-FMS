using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.UserFeature.Bogus.Command;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.UserEndpoints.Bogus;

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