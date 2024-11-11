using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Semester.Query;
using Module.Shared.Abstractions;

namespace Module.Semester.Endpoints.Semester;

public class GetSemestersByUserId : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/MySemesters/{userId:guid}", async (Guid userId, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new GetSemestersByUserIdQuery(userId));
        });
    }
}