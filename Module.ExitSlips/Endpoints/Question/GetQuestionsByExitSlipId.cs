using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.Question.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.Question
{
    public class GetQuestionsByExitSlipId : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:ExitSlipModule:Question:GetQuestionsByExitSlipId"] ??
                       throw new Exception("Route is not added to config file"),
                async ([FromRoute] Guid exitSlipId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetQuestionsByExitSlipIdQuery(exitSlipId))).ReturnHttpResult())
                .WithTags("Question")
                .RequireAuthorization();
        }
    }
}
