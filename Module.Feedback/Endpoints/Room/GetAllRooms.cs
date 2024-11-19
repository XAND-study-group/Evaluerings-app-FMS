using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Feedback.Application.Features.Room.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Room;

public class GetAllRooms : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/Rooms",
            async ([FromBody] IMediator mediator) => (await mediator.Send(new GetAllRoomsQuery())).ReturnHttpResult())
            .WithTags("Room")
            .RequireAuthorization();
    }
}