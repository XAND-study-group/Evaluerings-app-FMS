using School.Domain.Entities;
using SharedKernel.Dto.Features.School.Lecture.Query;

namespace School.Domain.Extension;

public static class LectureExtension
{
    public static GetSimpleLectureResponse MapToGetSimpleLectureResponse(this Lecture lecture)
    {
        return new GetSimpleLectureResponse(
            lecture.Title.Value,
            lecture.Description.Value,
            lecture.TimePeriod.From,
            lecture.TimePeriod.To,
            lecture.LectureDate.Value,
            lecture.ClassRoom.Value
        );
    }

    public static IEnumerable<GetSimpleLectureResponse> MapToIEnumerableGetLectureResponse(
        this IEnumerable<Lecture> lectures)
    {
        return lectures.Select(l => l.MapToGetSimpleLectureResponse());
    }
}