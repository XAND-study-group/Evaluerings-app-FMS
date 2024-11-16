using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Application.Features.Room.Query;
using SharedKernel.Interfaces;

namespace Module.Feedback.Endpoints.Room;

public class GetRoomsByClassId : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/Rooms/MyRooms/{classID:guid}", async (Guid classId, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new GetRoomsByClassIdQuery(classId));
        });
    }
}