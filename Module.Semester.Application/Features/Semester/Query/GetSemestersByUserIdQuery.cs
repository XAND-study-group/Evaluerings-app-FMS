using MediatR;
using SharedKernel.Dto.Features.Semester.Query;

namespace Module.Semester.Application.Features.Semester.Query;

public record GetSemestersByUserIdQuery(Guid UserId) : IRequest<IEnumerable<GetSemesterResponse>>;