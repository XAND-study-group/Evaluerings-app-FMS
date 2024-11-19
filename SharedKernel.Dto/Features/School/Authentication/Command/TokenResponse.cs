namespace SharedKernel.Dto.Features.Authentication.Command;

public record TokenResponse(
    string AccessToken,
    string RefreshToken);