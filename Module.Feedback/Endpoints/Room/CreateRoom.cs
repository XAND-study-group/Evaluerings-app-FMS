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

public class CreateRoom : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:FeedbackModule:Room:CreateRoom"] ??
                throw new Exception("Route is not added to config file"),
            async ([FromBody] CreateRoomRequest createRoomRequest, [FromServices] IMediator mediator) =>
            (await mediator.Send(new CreateRoomCommand(createRoomRequest))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization("RoomManagement");
    }
}