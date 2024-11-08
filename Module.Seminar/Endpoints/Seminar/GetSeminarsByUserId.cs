using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Module.Seminar.Application.Features.Seminar.Query;
using Module.Shared.Abstractions;

namespace Module.Seminar.Endpoints.Seminar;

public class GetSeminarsByUserId : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/MySeminars/{userId:guid}", async (Guid userId, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetSeminarsByUserIdQuery(userId));
                return response;
            }).WithTags("Seminar")
            .RequireAuthorization();
    }
}