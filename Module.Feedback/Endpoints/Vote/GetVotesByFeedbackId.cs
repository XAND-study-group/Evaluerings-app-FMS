using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Vote.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Vote;

public class GetVotesByFeedbackId : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:FeedbackModule:Vote:GetVotesByFeedbackId"] ??
                   throw new Exception("Route is not added to config file"),
                async (Guid feedbackId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetVotesByFeedbackIdQuery(feedbackId))).ReturnHttpResult())
            .WithTags("Vote")
            .RequireAuthorization("ReadFeedback");
    }
}