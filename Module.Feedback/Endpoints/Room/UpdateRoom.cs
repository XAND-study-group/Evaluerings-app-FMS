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

public class UpdateRoom : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPut(configuration["Routes:FeedbackModule:Room:UpdateRoom"] ??
                   throw new Exception("Route is not added to config file"),
                async ([FromBody] UpdateRoomRequest updateRoomRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new UpdateRoomCommand(updateRoomRequest))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization("RoomManagement");
    }
}