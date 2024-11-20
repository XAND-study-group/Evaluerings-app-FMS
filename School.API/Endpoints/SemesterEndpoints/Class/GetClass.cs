using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Class.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Class;

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