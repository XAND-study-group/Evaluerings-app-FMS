namespace Module.Semester.Application.Features.Class.Command.Dto;

public record CreateClassRequest(
    string Name,
    string Description,
    int StudentCapacity);