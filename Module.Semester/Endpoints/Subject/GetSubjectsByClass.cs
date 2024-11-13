using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Module.Semester.Application.Features.Subject.Query;
using Module.Shared.Abstractions;

namespace Module.Semester.Endpoints.Subject
{
    public class GetSubjectsByClass : IEndpoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapPost("/Semester/Class/GetSubjectsByClass",
                    async ([FromBody] GetSubjectsByClassRequest getSubjectsByClassRequest, [FromServices] IMediator mediator) =>
                    {
                        var response = await mediator.Send(new GetSubjectsByClassQuery(getSubjectsByClassRequest));
                        return Results.Ok(response);
                    }).WithTags("Class")
                .RequireAuthorization();
        }
    }
}
