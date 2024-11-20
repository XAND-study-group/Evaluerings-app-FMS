using MediatR;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Models;

namespace Module.Semester.Application.Features.Lecture.Query;

public record GetLectureQuery(Guid lectureId) : IRequest<Result<GetLectureResponse?>>;