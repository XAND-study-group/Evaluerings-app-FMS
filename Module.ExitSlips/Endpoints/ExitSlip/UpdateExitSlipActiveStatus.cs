using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Command;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Endpoints.ExitSlip
{
    public class UpdateExitSlipActiveStatus : IEndpoint
    {
        // TODO: FLytte Url til ConfigFil og tilføje Policies.
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapPut("ExitSlip",
                async ([FromBody] UpdateExitSlipActiveStatusRequest updateExitSLipActiveStatusRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new UpdateExitSlipActiveStatusCommand(updateExitSLipActiveStatusRequest))).ReturnHttpResult())
                .WithTags("ExitSlip")
                .RequireAuthorization();
        }
    }
}
