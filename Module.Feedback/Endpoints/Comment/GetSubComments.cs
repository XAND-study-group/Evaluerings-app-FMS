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
        app.MapGet("/Room/Feedback/Comment/{commentId:guid}/SubComments",
                async (Guid commentId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetSubCommentsQuery(commentId))).ReturnHttpResult())
            .WithTags("Comment")
            .RequireAuthorization("ReadInteractedFeedback");
    }
}