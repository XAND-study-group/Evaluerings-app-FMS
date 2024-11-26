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
    public class UpdateExitSlip : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            // TODO:
            app.MapPut("ExitSlip",
                async ([FromBody] UpdateExitSlipRequest updateExitSlipRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new UpdateExitSlipCommand(updateExitSlipRequest))).ReturnHttpResult())
                .WithTags("ExitSlip")
                .RequireAuthorization();
        }
    }
}
