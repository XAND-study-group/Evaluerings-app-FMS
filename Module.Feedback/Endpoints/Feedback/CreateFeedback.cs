using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Feedback.Command;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Feedback;

public class CreateFeedback : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:FeedbackModule:Feedback:CreateFeedback"] ??
                throw new Exception("Route is not added to config file"),
                async ([FromBody] CreateFeedbackRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateFeedbackCommand(request))).ReturnHttpResult())
            .WithTags("Feedback");
            //TODO: .RequireAuthorization("PostFeedback");
    }
}