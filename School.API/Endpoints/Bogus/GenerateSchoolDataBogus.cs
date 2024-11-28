using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.Bogus.Command;
using SharedKernel.Interfaces;

namespace School.API.Endpoints.Bogus;

public class GenerateSchoolDataBogus : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:UserModule:Bogus:GenerateSchoolDataBogus"] ??
                    throw new Exception("Route is not added to config file"),
            async ([FromServices] IMediator mediator) =>
            await mediator.Send(new GenerateSchoolDataCommand())).WithTags("GenerateData");
    }
}