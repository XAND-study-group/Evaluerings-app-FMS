using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Class.Command;
using SharedKernel.Dto.Features.Class.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Class;

public class AddTeacherToClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Semester/Class/AddTeacher",
            async ([FromBody] AddTeacherToClassRequest addTeacherToClassRequest, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new AddTeacherToClassCommand(addTeacherToClassRequest));
            }).WithName("Class")
            .RequireAuthorization();
    }
}