using SharedKernel.Dto.Features.School.ValueObjects;

namespace SharedKernel.Dto.Features.School.Lecture.Query;

public record GetSimpleLectureResponse(
    string Title,
    string Description,
    TimePeriodResponse TimePeriod,
    string ClassRoom);