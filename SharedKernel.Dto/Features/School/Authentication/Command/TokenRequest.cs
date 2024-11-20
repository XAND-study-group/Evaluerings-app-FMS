namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record TokenRequest(
    string AccessToken,
    string RefreshToken);