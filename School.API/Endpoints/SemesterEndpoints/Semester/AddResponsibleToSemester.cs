using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Semester.Command;
using SharedKernel.Dto.Features.School.Semester.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Semester;

public class AddResponsibleToSemester : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:SemesterModule:Semester:AddResponsibleToSemester"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] AddResponsibleToSemesterRequest request, [FromServices] IMediator mediator) =>
                (await mediator.Send(new AddResponsibleToSemesterCommand(request))).ReturnHttpResult())
            .WithTags("Semester")
            .RequireAuthorization("AdminOrTeacher");
    }
}