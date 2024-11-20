using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Vote.Query;
using SharedKernel.Dto.Features.Evaluering.Vote.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Vote;

public class GetVotesByFeedbackId : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet("/Room/Feedback/Votes",
                async ([FromBody] GetVotesByFeedbackIdRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetVotesByFeedbackIdQuery(request))).ReturnHttpResult())
            .WithTags("Vote")
            .RequireAuthorization();
    }
}