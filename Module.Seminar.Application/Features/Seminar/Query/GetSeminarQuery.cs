using MediatR;

namespace Module.Seminar.Application.Features.Seminar.Query;

public record GetSeminarQuery(Guid SeminarId) : IRequest<GetSeminarResponse>;