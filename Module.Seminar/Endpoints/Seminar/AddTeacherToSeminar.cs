using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Seminar.Application.Features.Seminar.Command;
using Module.Seminar.Application.Features.Seminar.Command.Dto;
using Module.Shared.Abstractions;

namespace Module.Seminar.Endpoints.Seminar;

public class AddTeacherToSeminar : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Seminar/AddTeacher",
            async ([FromBody] AddTeacherToSeminarRequest addTeacherToSeminarRequest, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new AddTeacherToSeminarCommand(addTeacherToSeminarRequest));
            }).WithName("Seminar")
            .RequireAuthorization();
    }
}