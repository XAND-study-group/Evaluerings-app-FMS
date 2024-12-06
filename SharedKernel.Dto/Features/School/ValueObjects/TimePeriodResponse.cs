namespace SharedKernel.Dto.Features.School.ValueObjects;

public record TimePeriodResponse(
    TimeOnly From,
    TimeOnly To,
    TimeSpan Duration);