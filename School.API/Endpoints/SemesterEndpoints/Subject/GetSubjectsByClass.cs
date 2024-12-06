using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Subject.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Subject;

public class GetSubjectsByClass : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:SemesterModule:Subject:GetSubjectsByClass"] ??
                   throw new Exception("Route is not added to config file"),
                async ([FromRoute] Guid classId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetSubjectsByClassQuery(new GetSubjectsByClassRequest(classId))))
                .ReturnHttpResult())
            .WithTags("Class")
            .RequireAuthorization();
    }
}