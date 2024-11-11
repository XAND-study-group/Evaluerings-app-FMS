namespace SharedKernel.Dto.Features.Class.Command;

public record CreateClassRequest(
    string Name,
    string Description,
    int StudentCapacity);