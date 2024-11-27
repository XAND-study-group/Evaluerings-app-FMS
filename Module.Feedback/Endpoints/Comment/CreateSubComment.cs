using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Comment.Command;
using SharedKernel.Dto.Features.Evaluering.Comment.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Comment;

public class CreateSubComment : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:FeedbackModule:Comment:CreateSubComment"] ??
                throw new Exception("Route is not added to config file"),
                async ([FromBody] CreateSubCommentRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateSubCommentCommand(request))).ReturnHttpResult())
            .WithTags("Comment")
            .RequireAuthorization("CommentOnFeedBack");
    }
}