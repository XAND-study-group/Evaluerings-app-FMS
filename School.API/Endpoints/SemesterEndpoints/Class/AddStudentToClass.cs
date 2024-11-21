using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Class.Command;
using SharedKernel.Dto.Features.School.Class.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Class;

public class AddStudentToClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:SemesterModule:Class:AddStudentToClass"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] AddStudentToClassRequest addStudentToClassRequest,
                        [FromServices] IMediator mediator) =>
                    (await mediator.Send(new AddStudentToClassCommand(addStudentToClassRequest))).ReturnHttpResult())
            .WithTags("Class")
            .RequireAuthorization("Admin");
    }
}