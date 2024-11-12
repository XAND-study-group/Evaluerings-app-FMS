namespace SharedKernel.Dto.Features.Authentication.Command;

public record AuthenticateAccountLoginRequest(string Email, string Password);