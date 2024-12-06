using SharedKernel.Dto.Features.School.Lecture.Query;

namespace SharedKernel.Dto.Features.School.Subject.Query;

public record GetDetailedSubjectResponse(
    Guid Id,
    string Name,
    string Description,
    IEnumerable<GetDetailedLectureResponse> Lectures)
{
    public GetDetailedSubjectResponse() : this(Guid.Empty, string.Empty, string.Empty,
        new List<GetDetailedLectureResponse>())
    {
    }
}