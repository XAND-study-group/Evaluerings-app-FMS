using MediatR;
using SharedKernel.Dto.Features.Lecture.Query;

namespace Module.Semester.Application.Features.Lecture.Query;

public record GetLectureQuery(Guid lectureId) : IRequest<GetLectureResponse>;