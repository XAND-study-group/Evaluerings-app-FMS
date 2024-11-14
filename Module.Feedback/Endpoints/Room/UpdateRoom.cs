using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Application.Features.Room.Command;
using SharedKernel.Dto.Features.Evaluering.Room.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Room;

public class UpdateRoom : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPut("/Room",
            async ([FromBody] UpdateRoomRequest updateRoomRequest, [FromServices] IMediator mediator) =>
            (await mediator.Send(new UpdateRoomCommand(updateRoomRequest))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization();
    }
}