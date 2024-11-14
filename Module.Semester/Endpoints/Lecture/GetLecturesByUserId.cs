using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Lecture.Query;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Lecture;

public class GetLecturesByUserId : IEndpoint
{
    public void MapEndpoint(WebApplication app)
    {
        app.MapGet("/Semester/Class/Subject/MyLectures/{userId:guid}",
            async (Guid userId, [FromServices] IMediator mediator) =>
            {
                await mediator.Send(new GetLecturesByUserIdQuery(userId));
            }).WithTags("Lecture")
            .RequireAuthorization();
    }
}