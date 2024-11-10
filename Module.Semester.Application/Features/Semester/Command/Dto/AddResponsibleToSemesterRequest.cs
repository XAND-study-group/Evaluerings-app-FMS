namespace Module.Semester.Application.Features.Semester.Command.Dto;

public record AddResponsibleToSemesterRequest(
    Guid SemesterId,
    Guid UserId);