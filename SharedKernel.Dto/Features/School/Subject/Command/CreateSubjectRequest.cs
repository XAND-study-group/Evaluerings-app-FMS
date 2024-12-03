namespace SharedKernel.Dto.Features.School.Subject.Command;

public record CreateSubjectRequest(
    string Name,
    string Description);