using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Application.Features.Feedback.Command;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Feedback;

public class CreateFeedback : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Room/Feedback",
                async ([FromBody] CreateFeedbackRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateFeedbackCommand(request))).ReturnHttpResult())
            .WithTags("Feedback")
            .RequireAuthorization();
    }
}