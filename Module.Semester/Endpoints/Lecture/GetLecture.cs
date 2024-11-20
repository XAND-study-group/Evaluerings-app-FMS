using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Lecture.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Lecture;

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