using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Interfaces;

namespace Module.ExitSlip.Endpoints.ExitSlip
{
    public class GetExitSlipById : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            // TODO: FLytte Url til ConfigFil og tilføje Policies. 

            app.MapGet("/ExitSlip/{exitSlipId:guid}",
                async (Guid exitSlipId, [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new GetExitSlipByIdQuery(exitSlipId)); 
                });

        }
    }
}
