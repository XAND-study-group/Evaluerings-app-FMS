using SharedKernel.Enums.Features.Semester;

namespace SharedKernel.Dto.Features.School.Semester.Query;

public record GetDetailedSemesterResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    int SemesterNumber,
    DateOnly StartDate,
    DateOnly EndDate,
    SchoolType School,
    IEnumerable<GetSemesterUserResponse> Responsibles)
{
    public GetDetailedSemesterResponse() : this(Guid.Empty, Array.Empty<byte>(), string.Empty, 0, new DateOnly(), new DateOnly(), SchoolType.Fredericia, new List<GetSemesterUserResponse>()) { }
}