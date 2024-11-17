using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Lecture.Command;
using SharedKernel.Dto.Features.Lecture.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Lecture;

public class CreateLecture : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapPost(configuration["Routes:SemesterModule:Lecture:CreateLecture"] ??
                    throw new Exception("Route is not added to config file"),
                async ([FromBody] CreateLectureRequest createLectureRequest, [FromServices] IMediator mediator) =>
                (await mediator.Send(new CreateLectureCommand(createLectureRequest))).ReturnHttpResult())
            .WithTags("Lecture")
            .RequireAuthorization("Teacher");
    }
}