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
    public class GetAllExitSlipsForSubject : IEndpoint
    {
        void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            // TODO: FLytte Url til ConfigFil og tilføje Policies. 

            app.MapGet("/ExitSlips/SubjectExitSLips/{subjectId:guid}",
                async (Guid subjectId, [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new GetAllExitSlipsforSubjectQuery(subjectId));
                });
        }
    }
}
