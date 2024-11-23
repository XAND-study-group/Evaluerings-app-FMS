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

public class AddClassToRoom : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPut("/Room/AddClass",
            async ([FromBody] AddClassToRoomRequest request, [FromServices] IMediator mediator) =>
            (await mediator.Send(new AddClassToRoomCommand(request))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization("RoomManagement");
    }
}