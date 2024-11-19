namespace SharedKernel.Dto.Features.Lecture.Query;

public record GetSimpleLectureResponse(
    string LectureTitle,
    string Description,
    TimeOnly From,
    TimeOnly To,
    DateOnly Date,
    string ClassRoom);