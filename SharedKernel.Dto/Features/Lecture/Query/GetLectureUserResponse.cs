namespace SharedKernel.Dto.Features.Lecture.Query;

public record GetLectureUserResponse(
    Guid Id,
    string Firstname,
    string Lastname);