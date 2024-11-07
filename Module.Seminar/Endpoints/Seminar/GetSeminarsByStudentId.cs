using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Module.Seminar.Application.Features.Seminar.Query;
using Module.Shared.Abstractions;

namespace Module.Seminar.Endpoints.Seminar;

public class GetSeminarsByStudentId : IEndpoint
{
    void IEndpoint.MapEndpoint(WebApplication app)
    {
        app.MapGet("/MySeminars/{studentId:guid}", async (Guid studentId, IMediator mediator) =>
        {
            var response = await mediator.Send(new GetSeminarsByStudentIdQuery(studentId));
            return response;
        }).WithTags("Seminar")
            .RequireAuthorization();
    }
}