namespace SharedKernel.Dto.Features.School.User.Query;

public record GetEmailsByUserIdsResponse(IEnumerable<string> Emails);