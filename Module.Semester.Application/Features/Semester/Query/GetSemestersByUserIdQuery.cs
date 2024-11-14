using MediatR;
using SharedKernel.Dto.Features.Semester.Query;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Semester.Query;

public record GetSemestersByUserIdQuery(Guid UserId) : IRequest<Result<IEnumerable<GetSemestersResponse>>>;