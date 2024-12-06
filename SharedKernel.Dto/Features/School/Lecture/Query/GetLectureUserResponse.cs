namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetLectureUserResponse(
    Guid Id,
    string Firstname,
    string Lastname)
{
    public GetLectureUserResponse() : this(Guid.Empty, string.Empty, string.Empty)
    {
    }
}