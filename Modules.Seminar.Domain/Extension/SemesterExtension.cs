using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Dto.Features.Semester.Query;

namespace Module.Semester.Domain.Extension
{
    public static class SemesterExtension
    {
        public static GetSimpleSemesterResponse MapToGetSimpleSemesterResponse(this Entities.Semester semester) =>
            new GetSimpleSemesterResponse(
                semester.Id,
                semester.RowVersion,
                semester.Name.Value,
                semester.SemesterNumber.Value,
                semester.EducationRange.Start,
                semester.EducationRange.End,
                semester.School
            );
        public static IEnumerable<GetSimpleSemesterResponse> MapToIEnumerableGetSemesterResponse(this IEnumerable<Entities.Semester> semesters) =>
            semesters.Select(s => s.MapToGetSimpleSemesterResponse());
    }
}
