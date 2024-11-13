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
    public class GetSubject : IEndpoint
    {
        public void MapEndpoint(WebApplication app)
        {
            app.MapPost("/Semester/Class/GetSubject",
                    async ([FromBody] GetSubjectRequest getSubjectRequest, [FromServices] IMediator mediator) =>
                    {
                        var response = await mediator.Send (new GetSubjectQuery(getSubjectRequest));
                        return Results.Ok(response);
                    }).WithTags("Class")
                .RequireAuthorization();
        }
    }
}
