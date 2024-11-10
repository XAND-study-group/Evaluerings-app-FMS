using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Class.Command;
using Module.Semester.Application.Features.Class.Command.Dto;
using Module.Shared.Abstractions;

namespace Module.Semester.Endpoints.Class;

public class AddTeacherToClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Class/AddTeacher",
            async ([FromBody] AddTeacherToClassRequest addTeacherToClassRequest, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new AddTeacherToClassCommand(addTeacherToClassRequest));
            }).WithName("Class")
            .RequireAuthorization();
    }
}