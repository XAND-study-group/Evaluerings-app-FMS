using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Lecture.Query;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Lecture;

public class GetLecture : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/Semester/Class/Subject/Lecture/{lectureId:guid}",
            async (Guid lectureId, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new GetLectureQuery(lectureId));
            }).WithTags("Lecture")
            .RequireAuthorization();
    }
}