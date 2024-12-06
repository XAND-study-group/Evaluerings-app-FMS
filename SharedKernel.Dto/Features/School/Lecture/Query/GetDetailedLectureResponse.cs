using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Dto.Features.School.ValueObjects;

namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetDetailedLectureResponse(
    Guid Id,
    byte[] RowVersion,
    string Title,
    string Description,
    TimePeriodResponse TimePeriod,
    DateOnly LectureDate,
    string ClassRoom,
    IEnumerable<GetSimpleUserResponse> Teachers);