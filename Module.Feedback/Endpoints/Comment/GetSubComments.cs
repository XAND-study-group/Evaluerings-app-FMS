using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Application.Features.Comment.Query;
using SharedKernel.Dto.Features.Evaluering.Comment.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Comment;

public class GetSubComments : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/Room/Feedback/Comment/SubComments",
                async ([FromBody] GetSubCommentsRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetSubCommentsQuery(request))).ReturnHttpResult())
            .WithTags("Comment")
            .RequireAuthorization();
    }
}