﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Interfaces;

namespace Module.ExitSlip.Endpoints.ExitSlip.Queries
{
    public class GetAllExitSlipsForLecture : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:ExitSlipModule:ExitSlip:GetAllExitSlipsForLecture"] ??
                       throw new ArgumentException("Route is not added to config file"),
                async (Guid lectureId, [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new GetAllExitSlipsForLectureQuery(lectureId));
                });
        }
    }
}