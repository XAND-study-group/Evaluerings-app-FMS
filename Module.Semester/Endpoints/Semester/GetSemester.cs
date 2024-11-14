using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Semester.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Semester;

public class GetSemester : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/Semester/{semesterId:guid}",
            async (Guid semesterId, [FromServices] IMediator mediator) =>
            (await mediator.Send(new GetSemesterQuery(semesterId))).ReturnHttpResult()).WithTags("Semester")
            .RequireAuthorization();
    }
}