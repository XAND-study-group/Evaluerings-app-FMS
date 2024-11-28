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
using Module.ExitSlip.Application.Features.Question.Command;
using SharedKernel.Dto.Features.Evaluering.Question.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.Question
{
    public class CreateQuestion : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapPost(configuration["Routes:ExitSlipModule:Question:CreateQuestion"] ??
                        throw new Exception("Route is not added to config file"),
                async ([FromBody]CreateQuestionRequest createQuestionRequest, [FromServices]IMediator mediator)=>
                (await mediator.Send(new CreateQuestionCommand(createQuestionRequest))).ReturnHttpResult())
                .WithTags("Question")
                .RequireAuthorization();
        }
    }
}
