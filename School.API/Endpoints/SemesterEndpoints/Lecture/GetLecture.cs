using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Lecture.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Lecture;

public class GetLecture : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:SemesterModule:Lecture:GetLecture"] ??
                   throw new Exception("Route is not added to config file"),
                async (Guid lectureId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetLectureQuery(lectureId))).ReturnHttpResult())
            .WithTags("Lecture")
            .RequireAuthorization();
    }
}