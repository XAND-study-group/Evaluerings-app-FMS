﻿using MediatR;
using School.Application.Features.SemesterFeature.Class.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Class;

public class GetClassesByUserId : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:SemesterModule:Class:GetClassesByUserId"] ??
                   throw new Exception("Route is not added to config file"),
                async (Guid userId, IMediator mediator) =>
                    (await mediator.Send(new GetClassesByUserIdQuery(userId))).ReturnHttpResult())
            .WithTags("Class")
            .RequireAuthorization();
    }
}