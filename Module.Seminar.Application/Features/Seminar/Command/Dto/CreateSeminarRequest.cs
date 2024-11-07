namespace Module.Seminar.Application.Features.Seminar.Command.Dto;

public record CreateSeminarRequest(
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate,
    int StudentCapacity);