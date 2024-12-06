using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Vote.Command;
using SharedKernel.Dto.Features.Evaluering.Vote.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Vote;

public class CreateVote : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:FeedbackModule:Vote:CreateVote"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] CreateVoteRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateVoteCommand(request))).ReturnHttpResult())
            .WithTags("Vote")
            .RequireAuthorization("VoteOnFeedback", "AssureUserInRoomOfFeedbackModification");
    }
}