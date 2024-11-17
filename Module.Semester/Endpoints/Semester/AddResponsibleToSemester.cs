using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Semester.Command;
using SharedKernel.Dto.Features.Semester.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Semester;

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