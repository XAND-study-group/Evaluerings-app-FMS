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
using SharedKernel.Dto.Features.Subject.Query;

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
