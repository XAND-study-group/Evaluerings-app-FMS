namespace SharedKernel.Dto.Features.School.Authentication.Command;

public record AddClaimToUserRequest(Guid UserId, string ClaimName, string ClaimValue);