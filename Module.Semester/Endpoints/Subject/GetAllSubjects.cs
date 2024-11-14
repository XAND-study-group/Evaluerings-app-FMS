using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Subject.Query;
using SharedKernel.Interfaces;

namespace Module.Semester.Endpoints.Subject
{
    public class GetAllSubjects : IEndpoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapGet("/Semester/Class/GetAllSubjects",
                    async ([FromServices] IMediator mediator) =>
                    {
                        var response = await mediator.Send(new GetAllSubjectsQuery());
                        return Results.Ok(response);
                    }).WithTags("Class")
                .RequireAuthorization("Admin");
        }
    }
}
