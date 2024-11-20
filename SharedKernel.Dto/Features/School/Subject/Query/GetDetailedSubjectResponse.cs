using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;

namespace SharedKernel.Dto.Features.School.Subject.Query
{
    public record GetDetailedSubjectResponse(
        Guid Id,
        string Name,
        string Description,
        IEnumerable<GetDetailedLectureResponse> Lectures);

}

