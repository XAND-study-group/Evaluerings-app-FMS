using MediatR;
using Module.Semester.Application.Features.Class.Query.Dto;

namespace Module.Semester.Application.Features.Class.Query;

public record GetClassesByUserIdQuery(Guid UserId) : IRequest<IEnumerable<GetClassesResponse>>;