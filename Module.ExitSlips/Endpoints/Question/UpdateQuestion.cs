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
            app.MapPut(configuration["Routes:ExitSlipModule:Question:UpdateQuestion"] ??
                       throw new Exception("Route is not added to config file"),
                async ([FromBody]UpdateQuestionRequest UpdateQuestionRequest, [FromServices]IMediator mediator)=>
                (await mediator.Send(new UpdateQuestionCommand(UpdateQuestionRequest))).ReturnHttpResult())
                .WithTags("Question")
                .RequireAuthorization();
        }
    }
}
