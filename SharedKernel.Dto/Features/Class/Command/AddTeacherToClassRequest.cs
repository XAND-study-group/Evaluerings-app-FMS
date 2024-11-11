namespace SharedKernel.Dto.Features.Class.Command;

public record AddTeacherToClassRequest(
    Guid ClassId,
    Guid TeacherId);