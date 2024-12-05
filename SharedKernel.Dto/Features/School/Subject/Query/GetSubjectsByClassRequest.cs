namespace SharedKernel.Dto.Features.School.Subject.Query;

public record GetSubjectsByClassRequest(
    Guid Id
)
{
    public GetSubjectsByClassRequest() : this(Guid.Empty) { }
}