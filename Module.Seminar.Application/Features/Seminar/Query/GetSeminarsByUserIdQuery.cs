using MediatR;
using Module.Seminar.Application.Features.Seminar.Query.Dto;

namespace Module.Seminar.Application.Features.Seminar.Query;

public record GetSeminarsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<GetSeminarsResponse>>;