using SharedKernel.Enums.Features.Semester;

namespace SharedKernel.Dto.Features.School.Semester.Query;

public record GetSimpleSemesterResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    int SemesterNumber,
    DateOnly StartDate,
    DateOnly EndDate,
    SchoolType School)
{
    public GetSimpleSemesterResponse() : this(Guid.Empty, Array.Empty<byte>(), string.Empty, 0, new DateOnly(), new DateOnly(), SchoolType.Fredericia) { }
}