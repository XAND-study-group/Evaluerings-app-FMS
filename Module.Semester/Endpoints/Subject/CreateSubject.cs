using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Subject.Command;
using SharedKernel.Dto.Features.Subject.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Subject;

public class CreateSubject : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapPost("/Semester/Class/AddSubject",
            async ([FromBody] CreateSubjectRequest createSubjectRequest, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new CreateSubjectCommand(createSubjectRequest));
            }).WithTags("Class")
            .RequireAuthorization("Admin");
    }
}