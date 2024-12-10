using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Models;

namespace Module.Feedback.Application.Services;

public interface ISchoolApiProxy
{
    Task<Result<IEnumerable<GetEmailsByUserIdsResponse>>> GetEmailsByUserIdsAsync(IEnumerable<Guid> userIds);
}