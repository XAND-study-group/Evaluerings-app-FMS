using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Seminar.Application.Features.Seminar.Query;
using Module.Shared.Abstractions;

namespace Module.Seminar.Endpoints.Seminar;

public class GetSeminar : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/Seminar/{seminarId:guid}",
            async (Guid seminarId, [FromServices] IMediator mediator) =>
            {
                var response = await mediator.Send(new GetSeminarQuery(seminarId));
                return response;
            }).WithName("Seminar")
            .RequireAuthorization();
    }
}