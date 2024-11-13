namespace SharedKernel.Dto.Features.Authentication.Command;

public record AddClaimToUserRequest(Guid UserId, string ClaimName, string ClaimValue);