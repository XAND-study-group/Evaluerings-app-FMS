namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record ChangePasswordRequest(Guid AccountLoginId, string NewPassword, string Code);