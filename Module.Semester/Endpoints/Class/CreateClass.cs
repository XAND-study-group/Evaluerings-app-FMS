using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Class.Command;
using Module.Shared.Abstractions;
using SharedKernel.Dto.Features.Class.Command;

namespace Module.Semester.Endpoints.Class;

public class CreateClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Class", async ([FromBody] CreateClassRequest createClassRequest, [FromServices] IMediator mediator) =>
            await mediator.Send(new CreateClassCommand(createClassRequest))
        ).WithTags("Class")
        .RequireAuthorization();
    }
}