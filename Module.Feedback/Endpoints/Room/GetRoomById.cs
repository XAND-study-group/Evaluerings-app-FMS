﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Application.Features.Room.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Room;

public class GetRoomById : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/Room/{roomId:guid}",
            async (Guid roomId, [FromBody] IMediator mediator) =>
            (await mediator.Send(new GetRoomByIdQuery(roomId))).ReturnHttpResult())
            .WithName("Room")
            .RequireAuthorization();
    }
}