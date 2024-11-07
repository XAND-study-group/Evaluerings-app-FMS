namespace Module.Seminar.Application.Features.Seminar.Command.Dto;

public record AddTeacherToSeminarRequest(
    Guid SeminarId,
    Guid TeacherId);