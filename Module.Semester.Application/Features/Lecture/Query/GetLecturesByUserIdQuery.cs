using MediatR;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Lecture.Query;

public record GetLecturesByUserIdQuery(Guid userId) : IRequest<Result<IEnumerable<GetSimpleLectureResponse>>>;