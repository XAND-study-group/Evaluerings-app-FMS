namespace SharedKernel.Dto.Features.Class.Query;

public record GetSimpleClassResponse(
    Guid Id,
    byte[] RowVersion,
    string Name,
    string Description,
    int StudentCapacity);