using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Lecture.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Lecture;

public class GetLecturesByUserId : IEndpoint
{
    public void MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:SemesterModule:Lecture:GetLecturesByUserId"] ??
                   throw new Exception("Route is not added to config file"),
                async (Guid userId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetLecturesByUserIdQuery(userId))).ReturnHttpResult())
            .WithTags("Lecture")
            .RequireAuthorization();
    }
}