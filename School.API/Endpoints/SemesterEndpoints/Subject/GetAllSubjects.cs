using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Subject.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Subject
{
    public class GetAllSubjects : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:SemesterModule:Subject:GetAllSubjects"] ??
                       throw new Exception("Route is not added to config file"),
                    async ([FromServices] IMediator mediator) =>
                    (await mediator.Send(new GetAllSubjectsQuery())).ReturnHttpResult())
                .WithTags("Class")
                .RequireAuthorization("Admin");
        }
    }
}