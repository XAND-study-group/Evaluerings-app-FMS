using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.Bogus.Command;
using SharedKernel.Interfaces;

namespace Module.ExitSlip.Endpoints.Bogus;

public class GenerateExitSlipDataBogus : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:ExitSlipModule:Bogus:GenerateExitSlipDataBogus"] ??
                    throw new ArgumentException("Route is not added to config file"),
            async ([FromServices] IMediator mediator) =>
            await mediator.Send(new GenerateExitSlipsDataCommand())).WithTags("GenerateData");
    }
}