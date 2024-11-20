using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Subject.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Subject
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