﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Module.Semester.Application.Features.Subject.Query;
using SharedKernel.Dto.Features.Subject.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace Module.Semester.Endpoints.Subject
{
    public class GetSubjectsByClass : IEndpoint
    {
        public void MapEndpoint(WebApplication app, IConfiguration configuration)
        {
            app.MapGet(configuration["Routes:SemesterModule:Subject:GetAllSubjectsByClass"] ??
                       throw new Exception("Route is not added to config file"),
                    async ([FromRoute] Guid id, [FromServices] IMediator mediator) =>
                    (await mediator.Send(new GetSubjectsByClassQuery(new GetSubjectsByClassRequest(id)))).ReturnHttpResult())
                .WithTags("Class")
                .RequireAuthorization();
        }
    }
}