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
    public class UpdateQuestion:IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapPut("/ExitSlip/Question",
                async([FromBody]UpdateQuestionRequest UpdateQuestionRequest, [FromServices]IMediator mediator)=>
                (await mediator.Send(new UpdateQuestionCommand(UpdateQuestionRequest))).ReturnHttpResult())
                .WithTags("Question")
                .RequireAuthorization();
        }
    }
}
