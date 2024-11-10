using MediatR;
using Module.Semester.Application.Features.Semester.Query.Dto;

namespace Module.Semester.Application.Features.Semester.Query;

public record GetSemestersByUserIdQuery(Guid UserId) : IRequest<IEnumerable<GetSemesterResponse>>;