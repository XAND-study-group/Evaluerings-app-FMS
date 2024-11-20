using MediatR;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Models;

namespace School.Application.Features.SemesterFeature.Lecture.Query;

public record GetLectureQuery(Guid lectureId) : IRequest<Result<GetDetailedLectureResponse?>>;