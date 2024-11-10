namespace Module.Semester.Application.Features.Class.Command.Dto;

public record AddTeacherToClassRequest(
    Guid ClassId,
    Guid TeacherId);