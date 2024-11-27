using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Feedback.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Feedback;

public class GetFeedbacksByClassIdOrderByCreatedDateTime : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:FeedbackModule:Feedback:GetFeedbacksByClassIdOrderByCreatedDateTime"] ??
                throw new Exception("Route is not added to config file"),
            async (Guid classId, [FromServices] IMediator mediator) =>
            (await mediator.Send(new GetFeedbacksByClassIdOrderByCreatedDateTimeQuery(classId))).ReturnHttpResult())
            .WithTags("Feedback")
            .RequireAuthorization("ReadFeedback");
    }
}