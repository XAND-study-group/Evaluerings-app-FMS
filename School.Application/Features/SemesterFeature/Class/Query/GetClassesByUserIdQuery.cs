using MediatR;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Class.Query;

public record GetClassesByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<GetSimpleClassResponse>>>;