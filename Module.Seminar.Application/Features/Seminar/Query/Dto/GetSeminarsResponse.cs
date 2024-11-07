namespace Module.Seminar.Application.Features.Seminar.Query.Dto;

public record GetSeminarsResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate,
    int StudentCapacity);