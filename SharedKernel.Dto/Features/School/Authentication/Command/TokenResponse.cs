namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record TokenResponse(
    string AccessToken,
    string RefreshToken);