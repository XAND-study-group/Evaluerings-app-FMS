﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Command;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.ExitSlip.Commands;

public class DeleteExitSlip : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapDelete(configuration["Routes:ExitSlipModule:ExitSlip:DeleteExitSlip"] ??
                      throw new ArgumentException("Route is not added to config file"),
                async ([FromBody] DeleteExitSlipRequest deleteExitSlipRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new DeleteExitSlipCommand(deleteExitSlipRequest))).ReturnHttpResult())
            .WithTags("ExitSlip")
            .RequireAuthorization();
    }
}