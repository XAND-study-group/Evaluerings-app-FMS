using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Interfaces;

namespace Module.Feedback.Endpoints.Room;

public class CreateRoom : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Room",
            async ([FromBody] CreateRoomRequest createRoomRequest, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new CreateRoomCommand(createRoomRequest));
            });
    }
}