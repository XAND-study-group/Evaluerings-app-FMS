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
    public class CreateAnswer : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapPost(configuration["Routes:ExitSlipModule:Answer:CreateAnswer"] ??
                        throw new Exception("Route is not added to config file"),
                    async ([FromBody] CreateAnswerRequest createAnswerRequest, [FromServices] IMediator mediator) =>
                    (await mediator.Send(new CreateAnswerCommand(createAnswerRequest))).ReturnHttpResult())
                .WithTags("Answer")
                .RequireAuthorization();
        }
    }
}
