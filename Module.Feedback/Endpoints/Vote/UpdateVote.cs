using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Application.Features.Vote.Command;
using SharedKernel.Dto.Features.Evaluering.Vote.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Vote;

public class UpdateVote : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPut("/Room/Feedback/UpdateVote",
            async ([FromBody] UpdateVoteRequest updateVoteRequest, [FromServices] IMediator mediator) =>
            (await mediator.Send(new UpdateVoteCommand(updateVoteRequest))).ReturnHttpResult())
            .WithTags("Vote")
            .RequireAuthorization();
    }
}