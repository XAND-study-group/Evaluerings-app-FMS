namespace Module.Semester.Application.Features.Class.Command.Dto;

public record AddStudentToClassRequest(
    Guid ClassId,
    Guid StudentId);