using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.ExitSlip.Application.Features.Answer.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.ExitSlip.Endpoints.Answer
{
    public class GetAnswersByQuestionId : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:ExitSlipModule:Answer:GetAnswersByQuestionId"] ??
                       throw new Exception("Route is not added to config file"),
                async ([FromRoute] Guid questionId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetAllAnswersForQuestionIdQuery(questionId))).ReturnHttpResult())
                .WithTags("Answer")
                .RequireAuthorization();
        }
    }
}
