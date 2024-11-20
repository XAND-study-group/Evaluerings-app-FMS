namespace SharedKernel.Dto.Features.School.Class.Command;

public record AddStudentToClassRequest(
    Guid ClassId,
    Guid StudentId);