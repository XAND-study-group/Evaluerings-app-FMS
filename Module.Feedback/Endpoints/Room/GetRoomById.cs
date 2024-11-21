using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Room.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Room;

public class GetRoomById : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet("/Room/{roomId:guid}",
            async (Guid roomId, [FromBody] IMediator mediator) =>
            (await mediator.Send(new GetRoomByIdQuery(roomId))).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization("ReadRoom");
    }
}