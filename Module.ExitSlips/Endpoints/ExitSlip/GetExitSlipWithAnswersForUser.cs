using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Endpoints.ExitSlip
{
    public class GetExitSlipWithAnswersForUser : IEndpoint
    {
        // TODO: FLytte Url til ConfigFil og tilføje Policies. 
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet("ExitSLip/{userId:guid}/{exitSlipId:guid}",
            async (Guid userId, Guid exitSlipId, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new GetExitSlipWithAnswersForUserQuery(userId, exitSlipId));
            });
        }
    }
}
