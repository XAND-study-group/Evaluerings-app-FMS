namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record SignOutRequest(
    Guid UserId,
    string RefreshToken);