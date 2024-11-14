using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Subject.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Subject
{
    public class GetSubjectsByClass : IEndpoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("/Semester/Class/GetSubjectsByClass/{id:guid}",
                    async ([FromRoute]Guid id,[FromServices] IMediator mediator) =>
                    {
                        var response = await mediator.Send(new GetSubjectsByClassQuery(new GetSubjectsByClassRequest(id)));
                        return Results.Ok(response);
                    }).WithTags("Class")
                .RequireAuthorization();
        }
    }
}
