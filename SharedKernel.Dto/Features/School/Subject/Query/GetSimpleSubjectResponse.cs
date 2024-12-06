using SharedKernel.Dto.Features.School.Lecture.Query;

namespace SharedKernel.Dto.Features.School.Subject.Query;

public record GetSimpleSubjectResponse(
    Guid Id,
    string Name,
    string Description,
    IEnumerable<GetSimpleLectureResponse> Lectures);