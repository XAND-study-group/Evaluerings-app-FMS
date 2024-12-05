using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Command;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.ExitSlip
{
    public class CreateExitSlip : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            // TODO: FLytte Url til ConfigFil og tilføje Policies. 

            app.MapPost("/ExitSlip",
                async ([FromBody] CreateExitSlipRequest createExitSlipRequest, [FromServices] IMediator mediator) =>
               (await mediator.Send(new CreateExitSlipCommand(createExitSlipRequest))).ReturnHttpResult())
                .WithTags("ExitSlip")
                .RequireAuthorization();
        }
    }
}
