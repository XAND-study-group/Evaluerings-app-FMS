namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetSimpleLectureResponse(
    string Title,
    string Description,
    TimeOnly FromTime,
    TimeOnly ToTime,
    DateOnly LectureDate,
    string ClassRoom);