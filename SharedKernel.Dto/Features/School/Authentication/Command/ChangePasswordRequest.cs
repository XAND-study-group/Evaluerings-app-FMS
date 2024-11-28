namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record ChangePasswordRequest(string NewPassword, string Code);