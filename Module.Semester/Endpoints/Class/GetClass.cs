using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Class.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Class;

public class GetClass : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:SemesterModule:Class:GetClass"] ??
                   throw new Exception("Route is not added to config file"),
                async ([FromRoute] Guid classId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetClassQuery(classId))).ReturnHttpResult())
            .WithTags("Class")
            .RequireAuthorization();
    }
}