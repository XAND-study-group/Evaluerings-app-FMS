namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetAllLecturesResponse(
    string LectureTitle,
    string Description,
    TimeOnly From,
    TimeOnly To,
    DateOnly Date,
    string ClassRoom);