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

public class DeleteVote : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapDelete("/Room/Feedback/DeleteVote",
            async ([FromBody] DeleteVoteRequest deleteVoteRequest, [FromServices] IMediator mediator) =>
            (await mediator.Send(new DeleteVoteCommand(deleteVoteRequest))).ReturnHttpResult())
            .WithTags("Vote")
            .RequireAuthorization("VoteOnFeedback");
    }
}