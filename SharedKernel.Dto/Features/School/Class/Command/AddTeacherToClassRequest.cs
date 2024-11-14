namespace SharedKernel.Dto.Features.School.Class.Command;

public record AddTeacherToClassRequest(
    Guid ClassId,
    Guid TeacherId);