using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Lecture.Command;
using SharedKernel.Dto.Features.School.Lecture.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Lecture;

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