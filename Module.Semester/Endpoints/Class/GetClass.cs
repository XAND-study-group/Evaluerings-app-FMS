using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Class.Query;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Class;

public class GetClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/Semester/Class/{classId:guid}",
            async ([FromRoute] Guid classId, [FromServices] IMediator mediator) =>
            {
                var response = await mediator.Send(new GetClassQuery(classId));
                return response;
            }).WithName("Class")
            .RequireAuthorization();
    }
}