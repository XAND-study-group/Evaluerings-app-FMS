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

public class CreateComment : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost("/Room/Feedback/CreateComment",
                async ([FromBody] CreateCommentRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateCommentCommand(request))).ReturnHttpResult())
            .WithTags("Comment")
            .RequireAuthorization();
    }
}