namespace SharedKernel.Dto.Features.School.ValueObjects;

public record EducationRangeResponse(
    DateOnly Start,
    DateOnly End);