namespace SharedKernel.Dto.Features.School.Semester.Query;

public record GetSemesterUserResponse(
    Guid Id)
{
    public GetSemesterUserResponse() : this(Guid.Empty)
    {
    }
}