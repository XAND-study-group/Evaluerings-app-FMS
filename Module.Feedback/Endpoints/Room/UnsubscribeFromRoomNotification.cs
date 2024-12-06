using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Room.Command;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Room;

public class UnsubscribeFromRoomNotification : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPut(configuration["Routes:FeedbackModule:Room:UnsubscribeToRoomNotification"] ??
                   throw new Exception("Rute ikke tilføjet til konfig fil"), async (
                    [FromBody] UnsubscribeToRoomNotificationRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new UnsubscribeFromRoomNotificationCommand(request))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization("RoomManagement");
    }
}