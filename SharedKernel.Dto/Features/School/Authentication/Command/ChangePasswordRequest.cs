namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record ChangePasswordRequest(
    Guid UserId,
    string NewPassword,
    byte[] rowVersion);