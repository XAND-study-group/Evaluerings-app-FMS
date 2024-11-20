using MediatR;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Lecture.Query;

public record GetLecturesByUserIdQuery(Guid userId) : IRequest<Result<IEnumerable<GetSimpleLectureResponse>>>;