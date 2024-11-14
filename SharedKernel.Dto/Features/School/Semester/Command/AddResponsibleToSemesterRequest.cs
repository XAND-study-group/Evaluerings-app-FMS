namespace SharedKernel.Dto.Features.School.Semester.Command;

public record AddResponsibleToSemesterRequest(
    Guid SemesterId,
    Guid UserId);