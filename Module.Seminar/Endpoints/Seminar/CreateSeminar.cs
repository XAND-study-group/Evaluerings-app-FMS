using MediatR;
using Module.Seminar.Application.Features.Seminar.Command;
using Module.Seminar.Application.Features.Seminar.Command.Dto;
using Module.Shared.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Module.Seminar.Endpoints.Seminar;

public class CreateSeminar : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Seminar", async ([FromBody] CreateSeminarRequest createSeminarRequest, [FromServices] IMediator mediator) =>
            await mediator.Send(new CreateSeminarCommand(createSeminarRequest))
        ).WithTags("Seminar")
        .RequireAuthorization();
    }
}