namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record AuthenticateAccountLoginRequest(
    string Email,
    string Password);