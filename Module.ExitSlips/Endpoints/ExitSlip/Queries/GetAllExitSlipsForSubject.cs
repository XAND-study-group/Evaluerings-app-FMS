﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.ExitSlip.Queries;

public class GetAllExitSlipsForSubject : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:ExitSlipModule:ExitSlip:GetAllExitSlipsForSubject"] ??
                   throw new ArgumentException("Route is not added to config file"),
                async (Guid subjectId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetAllExitSlipsforSubjectQuery(subjectId))).ReturnHttpResult())
            .WithTags("ExitSlip");
    }
}