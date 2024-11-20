using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Semester.Domain.Entities;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;

namespace Module.Semester.Domain.Extension
{
    public static class LectureExtension
    {
        public static GetSimpleLectureResponse MapToGetSimpleLectureResponse(this Lecture lecture) =>
            new GetSimpleLectureResponse(
                lecture.Title.Value,
                lecture.Description.Value,
                lecture.TimePeriod.From,
                lecture.TimePeriod.To,
                lecture.LectureDate.Value,
                lecture.ClassRoom.Value
            );

        public static IEnumerable<GetSimpleLectureResponse> MapToIEnumerableGetLectureResponse(this IEnumerable<Lecture> lectures) =>
            lectures.Select(l => l.MapToGetSimpleLectureResponse());
    }
}
