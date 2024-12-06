using SharedKernel.Dto.Features.School.User.Query;

namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetDetailedLectureResponse(
    Guid Id,
    byte[] RowVersion,
    string Title,
    string Description,
    TimeOnly FromTime,
    TimeOnly ToTime,
    TimeSpan Duration,
    DateOnly LectureDate,
    string ClassRoom,
    IEnumerable<GetSimpleUserResponse> Teachers);