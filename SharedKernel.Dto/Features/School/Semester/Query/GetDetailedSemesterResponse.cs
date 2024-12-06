using SharedKernel.Dto.Features.School.ValueObjects;
using SharedKernel.Enums.Features.Semester;

namespace SharedKernel.Dto.Features.School.Semester.Query;

public record GetDetailedSemesterResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    int SemesterNumber,
    EducationRangeResponse EducationRange,
    SchoolType School,
    IEnumerable<GetSemesterUserResponse> SemesterResponsibles);