using MediatR;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Semester.Query;

public record GetSemestersByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<GetSimpleSemesterResponse>>>;