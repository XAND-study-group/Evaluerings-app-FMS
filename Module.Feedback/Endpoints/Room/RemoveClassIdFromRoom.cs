using Azure.Core;
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

public class RemoveClassIdFromRoom : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPut(configuration["Routes:FeedbackModule:Room:RemoveClassIdFromRoom"] ??
                   throw new Exception("Rute er ikke tilføjet til config filen"),
                async ([FromBody] RemoveClassIdFromRoomRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new RemoveClassIdFromRoomCommand(request))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization("RoomManagement");
    }
}