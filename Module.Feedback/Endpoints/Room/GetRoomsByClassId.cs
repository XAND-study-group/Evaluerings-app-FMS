using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Room.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Room;

public class GetRoomsByClassId : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:FeedbackModule:Room:GetRoomsByClassId"] ??
                   throw new Exception("Route is not added to config file"),
                async (Guid classId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetRoomsByClassIdQuery(classId))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization("ReadRoom");
    }
}