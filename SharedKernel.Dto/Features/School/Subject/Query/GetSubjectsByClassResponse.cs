namespace SharedKernel.Dto.Features.School.Subject.Query;

public record GetSubjectsByClassResponse(
    string ClassName,
    IEnumerable<GetDetailedSubjectResponse> Subjects)
{
    public GetSubjectsByClassResponse() : this(string.Empty, new List<GetDetailedSubjectResponse>()) { }
}


