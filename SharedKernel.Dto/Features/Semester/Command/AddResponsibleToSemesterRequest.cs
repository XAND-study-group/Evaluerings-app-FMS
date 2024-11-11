namespace SharedKernel.Dto.Features.Semester.Command;

public record AddResponsibleToSemesterRequest(
    Guid SemesterId,
    Guid UserId);