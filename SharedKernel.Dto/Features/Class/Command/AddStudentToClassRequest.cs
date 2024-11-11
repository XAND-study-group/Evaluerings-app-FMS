namespace SharedKernel.Dto.Features.Class.Command;

public record AddStudentToClassRequest(
    Guid ClassId,
    Guid StudentId);