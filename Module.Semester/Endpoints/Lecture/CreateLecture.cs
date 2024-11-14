using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Lecture.Command;
using SharedKernel.Dto.Features.School.Lecture.Command;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Lecture;

public class CreateLecture : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapPost("/Semester/Class/Subject/Lecture", async ([FromBody]CreateLectureRequest createLectureRequest, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(new CreateLectureCommand(createLectureRequest));
        }).WithTags("Lecture")
        .RequireAuthorization();
    }
}