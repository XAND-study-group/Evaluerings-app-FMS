using SharedKernel.Dto.Features.School.User.Query;

namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetDetailedLectureResponse(
    Guid Id,
    byte[] RowVersion,
    string LectureTitle,
    string Description,
    TimeOnly FromTime,
    TimeOnly ToTime,
    DateOnly Date,
    string ClassRoom,
    IEnumerable<GetSimpleUserResponse> Teachers)
{
    public GetDetailedLectureResponse() : this(Guid.Empty, Array.Empty<byte>(), string.Empty, string.Empty, TimeOnly.MinValue, TimeOnly.MinValue, DateOnly.MinValue, string.Empty, new List<GetSimpleUserResponse>()) { }
}