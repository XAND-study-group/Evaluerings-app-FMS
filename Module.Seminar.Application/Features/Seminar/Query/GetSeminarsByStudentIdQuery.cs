using MediatR;
using Module.Seminar.Application.Features.Seminar.Query.Dto;

namespace Module.Seminar.Application.Features.Seminar.Query;

public record GetSeminarsByStudentIdQuery(Guid StudentId) : IRequest<IEnumerable<GetSeminarsResponse>>;