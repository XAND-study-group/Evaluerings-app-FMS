using SharedKernel.Dto.Features.School.Lecture.Query;

namespace SharedKernel.Dto.Features.School.Subject.Query
{
    public record GetAllSubjectsResponse(
        Guid Id,
        string Name,
        string Description,
        IEnumerable<GetLectureIdResponse> Lectures);

}

