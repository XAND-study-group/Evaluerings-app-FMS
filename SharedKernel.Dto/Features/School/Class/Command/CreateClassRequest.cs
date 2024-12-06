namespace SharedKernel.Dto.Features.School.Class.Command;

public record CreateClassRequest(
    string Name,
    string Description,
    int StudentCapacity,
    Guid SemesterId);