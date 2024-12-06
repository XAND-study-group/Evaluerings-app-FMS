namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetLectureIdResponse(Guid Id)
{
    public GetLectureIdResponse() : this(Guid.Empty)
    {
    }
}