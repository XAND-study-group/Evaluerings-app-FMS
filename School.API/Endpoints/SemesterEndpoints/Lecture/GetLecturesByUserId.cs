using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Lecture.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Lecture;

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