namespace SharedKernel.Dto.Features.Class.Query;

public record GetClassesResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    int StudentCapacity);