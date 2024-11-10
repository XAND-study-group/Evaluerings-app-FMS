namespace Module.Semester.Application.Features.Class.Query.Dto;

public record GetClassesResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    int StudentCapacity);