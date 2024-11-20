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
        app.MapGet("/AllFeedbacks/ForClass/{classId:guid}",
            async (Guid classId, [FromServices] IMediator mediator) =>
            (await mediator.Send(new GetFeedbacksByClassIdOrderByCreatedDateTimeQuery(classId))).ReturnHttpResult())
            .WithTags("Feedback")
            .RequireAuthorization();
    }
}