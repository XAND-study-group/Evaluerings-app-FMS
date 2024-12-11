using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Comment.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Comment;

public class GetSubComments : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:FeedbackModule:Comment:GetSubComments"] ??
                   throw new Exception("Route is not added to config file"),
                async (Guid commentId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetSubCommentsQuery(commentId))).ReturnHttpResult())
            .WithTags("Comment")
            .RequireAuthorization("ReadFeedback");
    }
}