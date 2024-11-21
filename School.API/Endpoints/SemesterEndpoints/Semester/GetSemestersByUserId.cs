using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Application.Features.SemesterFeature.Semester.Query;
using SharedKernel.Interfaces;
using SharedKernel.Models.Extensions;

namespace School.API.Endpoints.SemesterEndpoints.Semester;

public class GetSemestersByUserId : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app, IConfiguration configuration)
    {
        app.MapGet(configuration["Routes:SemesterModule:Semester:GetSemestersByUserId"] ??
                   throw new Exception("Route is not added to config file"),
                async (Guid userId, [FromServices] IMediator mediator) =>
                (await mediator.Send(new GetSemestersByUserIdQuery(userId))).ReturnHttpResult())
            .WithTags("Semester")
            .RequireAuthorization();
    }
}