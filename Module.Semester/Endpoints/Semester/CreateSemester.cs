﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Semester.Command;
using SharedKernel.Dto.Features.School.Semester.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Semester;

public class CreateSemester : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:SemesterModule:Semester:CreateSemester"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] CreateSemesterRequest createSemesterRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateSemesterCommand(createSemesterRequest))).ReturnHttpResult())
            .WithTags("Semester")
            .RequireAuthorization("Admin");
    }
}