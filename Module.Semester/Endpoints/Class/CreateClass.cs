using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Class.Command;
using SharedKernel.Dto.Features.School.Class.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Class;

public class CreateClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Semester/Class", async ([FromBody] CreateClassRequest createClassRequest, [FromServices] IMediator mediator) =>
            await mediator.Send(new CreateClassCommand(createClassRequest))
        ).WithTags("Class")
        .RequireAuthorization();
    }
}