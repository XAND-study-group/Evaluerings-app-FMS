namespace Module.Seminar.Application.Features.Seminar.Command.Dto;

public record AddStudentToSeminarRequest(
    Guid SeminarId,
    Guid StudentId);