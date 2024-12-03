using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Subject.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Subject
{
    public class GetSubject : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:SemesterModule:Subject:GetSubject"] ??
                       throw new Exception("Route is not added to config file"),
                    async ([FromRoute] Guid subjectId, [FromServices] IMediator mediator) =>
                    (await mediator.Send(new GetSubjectQuery(new GetSubjectRequest(subjectId)))).ReturnHttpResult())
                .WithTags("Class")
                .RequireAuthorization();
        }
    }
}