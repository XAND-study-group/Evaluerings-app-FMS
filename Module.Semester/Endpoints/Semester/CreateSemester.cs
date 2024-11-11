﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Semester.Command;
using Module.Shared.Abstractions;
using SharedKernel.Dto.Features.Semester.Command;

namespace Module.Semester.Endpoints.Semester;

public class CreateSemester : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Semester",
            async ([FromBody] CreateSemesterRequest createSemesterRequest, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new CreateSemesterCommand(createSemesterRequest));
            });
    }
}