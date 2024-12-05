namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record RequestResetPasswordResponse(
    string Code,
    byte[] RowVersion);