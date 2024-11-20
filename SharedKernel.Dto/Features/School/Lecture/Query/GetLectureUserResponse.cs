namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetLectureUserResponse(
    Guid Id,
    string Firstname,
    string Lastname);