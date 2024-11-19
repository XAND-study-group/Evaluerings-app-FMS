namespace SharedKernel.Dto.Features.Authentication.Command;

public record TokenRequest(
    string AccessToken,
    string RefreshToken);