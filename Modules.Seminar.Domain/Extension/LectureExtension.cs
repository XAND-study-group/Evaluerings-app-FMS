using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Semester.Domain.Entities;
using SharedKernel.Dto.Features.Lecture.Query;

namespace Module.Semester.Domain.Extension
{
    public static class LectureExtension
    {
        public static GetLectureResponse MapToGetLectureResponse(this Lecture lecture) =>
            new GetLectureResponse(
                lecture.Id,
                lecture.RowVersion,
                lecture.Title.Value,
                lecture.Description.Value,
                lecture.TimePeriod.From,
                lecture.TimePeriod.To,
                lecture.LectureDate.Value,
                lecture.ClassRoom.Value,
                lecture.Teachers.Select(t => t.MapToGetUserResponse())
            );

        public static IEnumerable<GetLectureResponse> MapToIEnumerableGetLectureResponse(this IEnumerable<Lecture> lectures) =>
            lectures.Select(l => l.MapToGetLectureResponse());
    }
}
