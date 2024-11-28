using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Feedback.Application.Features.Room.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Feedback.Endpoints.Room;

public class GetAllRooms : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:FeedbackModule:Room:GetAllRooms"] ??
                throw new Exception("Route is not added to config file"),
            async ([FromServices] IMediator mediator) => (await mediator.Send(new GetAllRoomsQuery())).ReturnHttpResult())
            .WithTags("Room");
        
        // TODO: REMEMBER ADD REQUIRE AUTHORIZATION
    }
}