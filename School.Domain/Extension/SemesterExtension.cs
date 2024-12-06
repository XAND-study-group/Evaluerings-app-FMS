using School.Domain.Entities;
using SharedKernel.Dto.Features.School.Semester.Query;

namespace School.Domain.Extension;

public static class SemesterExtension
{
    public static GetSimpleSemesterResponse MapToGetSimpleSemesterResponse(this Semester semester)
    {
        return new GetSimpleSemesterResponse(
            semester.Id,
            semester.RowVersion,
            semester.Name.Value,
            semester.SemesterNumber.Value,
            semester.EducationRange.Start,
            semester.EducationRange.End,
            semester.School
        );
    }

    public static IEnumerable<GetSimpleSemesterResponse> MapToIEnumerableGetSemesterResponse(
        this IEnumerable<Semester> semesters)
    {
        return semesters.Select(s => s.MapToGetSimpleSemesterResponse());
    }
}