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
using Module.ExitSlip.Application.Features.Answer.Command;
using SharedKernel.Dto.Features.Evaluering.Answer.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.Answer
{
    public class UpdateAnswer : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapPut(configuration["Routes:ExitSlipModule:Answer:UpdateAnswerOnQuestion"] ??
                       throw new Exception("Route is not added to config file"),
                    async ([FromBody] UpdateAnswerRequest updateAnswerRequest, [FromServices] IMediator mediator) =>
                    (await mediator.Send(new UpdateAnswerCommand(updateAnswerRequest))).ReturnHttpResult())
                .WithTags("Answer")
                .RequireAuthorization();
        }
    }
}
