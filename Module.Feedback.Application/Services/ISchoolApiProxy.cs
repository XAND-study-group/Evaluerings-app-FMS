namespace Module.Feedback.Application.Services;

public interface ISchoolApiProxy
{
    Task<IEnumerable<string>> GetEmailsByUserIdsAsync(IEnumerable<Guid> userIds);
}