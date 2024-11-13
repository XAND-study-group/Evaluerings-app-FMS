namespace SharedKernel.Dto.Features.Authentication.Command;

public record ChangePasswordRequest(Guid AccountLoginId, string NewPassword);