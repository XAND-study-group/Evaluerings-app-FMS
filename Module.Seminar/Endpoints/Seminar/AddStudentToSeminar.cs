using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Seminar.Application.Features.Seminar.Command;
using Module.Seminar.Application.Features.Seminar.Command.Dto;
using Module.Shared.Abstractions;

namespace Module.Seminar.Endpoints.Seminar;

public class AddStudentToSeminar : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("Seminar/AddStudent",
                async ([FromBody] AddStudentToSeminarRequest addStudentToSeminarRequest, [FromServices] IMediator mediator) =>
                {
                    await mediator.Send(new AddStudentToSeminarCommand(addStudentToSeminarRequest));
                }).WithTags("Seminar")
            .RequireAuthorization();
    }
}