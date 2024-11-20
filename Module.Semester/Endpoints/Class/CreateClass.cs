using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Class.Command;
using SharedKernel.Dto.Features.School.Class.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Class;

public class CreateClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:SemesterModule:Class:CreateClass"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] CreateClassRequest createClassRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateClassCommand(createClassRequest))).ReturnHttpResult())
            .WithTags("Class")
            .RequireAuthorization("Admin");
    }
}