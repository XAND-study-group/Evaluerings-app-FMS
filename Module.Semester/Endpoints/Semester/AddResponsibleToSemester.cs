using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Semester.Command;
using SharedKernel.Dto.Features.Semester.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Semester;

public class AddResponsibleToSemester : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Semester/AddResponsible",
            async ([FromBody] AddResponsibleToSemesterRequest request, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new AddResponsibleToSemesterCommand(request));
            }).WithName("Semester")
            .RequireAuthorization();
    }
}