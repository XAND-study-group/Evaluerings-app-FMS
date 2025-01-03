﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Feedback.Command;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Feedback;

public class DeleteFeedback : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapDelete(configuration["Routes:FeedbackModule:Feedback:DeleteFeedback"] ??
                      throw new Exception("Route is not added to config file"),
                async ([FromBody] DeleteFeedbackRequest deleteFeedbackRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new DeleteFeedbackCommand(deleteFeedbackRequest))).ReturnHttpResult())
            .WithTags("Feedback")
            .RequireAuthorization("Admin");
    }
}