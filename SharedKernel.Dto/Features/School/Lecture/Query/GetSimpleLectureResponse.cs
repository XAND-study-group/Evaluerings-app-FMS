namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetSimpleLectureResponse(
    string LectureTitle,
    string Description,
    TimeOnly From,
    TimeOnly To,
    DateOnly Date,
    string ClassRoom);