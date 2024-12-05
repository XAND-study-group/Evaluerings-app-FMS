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

public class ChangeFeedbackStatus : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPut(configuration["Routes:FeedbackModule:Feedback:ChangeFeedbackStatus"] ??
                   throw new Exception("Rute ikke tilføjet til konfig filen"),
            async ([FromBody] ChangeFeedbackStatusRequest request, [FromServices] IMediator mediator) =>
            (await mediator.Send(new ChangeFeedbackStatusCommand(request))).ReturnHttpResult()
        ).WithTags("Feedback")
        .RequireAuthorization("Admin");
    }
}