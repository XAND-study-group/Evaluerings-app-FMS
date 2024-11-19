﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Application.Features.Comment.Command;
using SharedKernel.Dto.Features.Evaluering.Comment.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Comment;

public class CreateSubComment : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/Room/Feedback/CreateSubComment",
                async ([FromBody] CreateSubCommentRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateSubCommentCommand(request))).ReturnHttpResult())
            .WithTags("Comment")
            .RequireAuthorization();
    }
}