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

public class DeleteRoom : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapDelete(configuration["Routes:FeedbackModule:Room:DeleteRoom"] ??
                throw new Exception("Route is not added to config file"),
            async ([FromBody] DeleteRoomRequest deleteRoomRequest, [FromServices] IMediator mediator) =>
            (await mediator.Send(new DeleteRoomCommand(deleteRoomRequest))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization("RoomManagement");
    }
}