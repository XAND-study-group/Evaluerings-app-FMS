namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record ResetPasswordRequest(string NewPassword, string Code);