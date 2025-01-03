﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Vote.Command;
using SharedKernel.Dto.Features.Evaluering.Vote.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Vote;

public class UpdateVote : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPut(configuration["Routes:FeedbackModule:Vote:UpdateVote"] ??
                   throw new Exception("Route is not added to config file"),
                async ([FromBody] UpdateVoteRequest updateVoteRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new UpdateVoteCommand(updateVoteRequest))).ReturnHttpResult())
            .WithTags("Vote")
            .RequireAuthorization("VoteOnFeedback");
    }
}