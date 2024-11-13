using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Subject.Query;
using Module.Shared.Abstractions;
using SharedKernel.Dto.Features.Subject.Command;

namespace Module.Semester.Endpoints.Subject
{
    public class GetAllSubjects : IEndpoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapPost("/Semester/Class/GetAllSubjects",
                    async ([FromBody] GetAllSubjectsRequest getAllSubjectsRequest, [FromServices] IMediator mediator) =>
                    {
                        var response = await mediator.Send(new GetAllSubjectsQuery());
                        return Results.Ok(response);
                    }).WithTags("Class")
                .RequireAuthorization();
        }
    }
}
