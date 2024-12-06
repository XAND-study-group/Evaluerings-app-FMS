namespace SharedKernel.Dto.Features.School.Class.Query;

public record GetClassSubjectResponse(
    Guid Id)
{
    public GetClassSubjectResponse() : this(Guid.Empty)
    {
    }
}