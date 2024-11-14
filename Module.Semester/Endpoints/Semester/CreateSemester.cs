using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Semester.Command;
using SharedKernel.Dto.Features.Semester.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Semester;

public class CreateSemester : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Semester",
            async ([FromBody] CreateSemesterRequest createSemesterRequest, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new CreateSemesterCommand(createSemesterRequest));
            }).WithTags("Semester")
            .RequireAuthorization();
    }
}