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

namespace Module.ExitSlip.Endpoints.ExitSlip.Commands
{
    public class CreateExitSlip : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapPost(configuration["Routes:ExitSlipModule:ExitSlip:CreateExitSlip"] ??
                       throw new ArgumentException("Route is not added to config file"),
                async ([FromBody] CreateExitSlipRequest createExitSlipRequest, [FromServices] IMediator mediator) =>
               (await mediator.Send(new CreateExitSlipCommand(createExitSlipRequest))).ReturnHttpResult())
                .WithTags("ExitSlip")
                .RequireAuthorization();
        }
    }
}
