namespace SharedKernel.Dto.Features.School.Subject.Query;

public record GetSubjectsByClassResponse(
    string Name,
    IEnumerable<GetDetailedSubjectResponse> Subjects);