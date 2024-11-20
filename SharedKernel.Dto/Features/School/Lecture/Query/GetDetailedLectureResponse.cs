using SharedKernel.Dto.Features.School.User.Query;

namespace SharedKernel.Dto.Features.Lecture.Query;

public record GetDetailedLectureResponse(
    Guid Id,
    byte[] RowVersion,
    string LectureTitle,
    string Description,
    TimeOnly FromTime,
    TimeOnly ToTime,
    DateOnly Date,
    string ClassRoom,
    IEnumerable<GetSimpleUserResponse> Teachers);