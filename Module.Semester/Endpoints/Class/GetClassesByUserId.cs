using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Module.Semester.Application.Features.Class.Query;
using Module.Shared.Abstractions;

namespace Module.Semester.Endpoints.Class;

public class GetClassesByUserId : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/MyClasses/{userId:guid}", async (Guid userId, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetClassesByUserIdQuery(userId));
                return response;
            }).WithTags("Class")
            .RequireAuthorization();
    }
}