using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Class.Command;
using SharedKernel.Dto.Features.School.Class.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Class;

public class AddStudentToClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Semester/Class/AddStudent",
                async ([FromBody] AddStudentToClassRequest addStudentToClassRequest, [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new AddStudentToClassCommand(addStudentToClassRequest));
                }).WithTags("Class")
            .RequireAuthorization();
    }
}