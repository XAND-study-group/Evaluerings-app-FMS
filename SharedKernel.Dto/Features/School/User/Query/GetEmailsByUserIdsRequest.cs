namespace SharedKernel.Dto.Features.School.User.Query;

public record GetEmailsByUserIdsRequest(IEnumerable<Guid> UserIds);