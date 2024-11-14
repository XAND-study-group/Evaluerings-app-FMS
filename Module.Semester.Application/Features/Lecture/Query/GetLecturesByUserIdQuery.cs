using MediatR;
using Module.Shared.Models;
using SharedKernel.Dto.Features.School.Lecture.Query;

namespace Module.Semester.Application.Features.Lecture.Query;

public record GetLecturesByUserIdQuery(Guid userId) : IRequest<Result<IEnumerable<GetAllLecturesResponse>>>;