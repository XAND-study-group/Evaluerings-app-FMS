using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using SharedKernel.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using Module.ExitSlip.Application.Features.ExitSlip.Command;
using SharedKernel.Models.Extensions;
using Microsoft.AspNetCore.Http;

namespace Module.ExitSlip.Endpoints.ExitSlip
{
    public class CreateExitSlip : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            // TODO

            app.MapPost("/ExitSlip",
                async ([FromBody] CreateExitSlipRequest createExitSlipRequest, [FromServices] IMediator mediator) =>
               (await mediator.Send(new CreateExitSlipCommand(createExitSlipRequest))).ReturnHttpResult())
                .WithTags("ExitSlip")
                .RequireAuthorization();
        }
    }
}
