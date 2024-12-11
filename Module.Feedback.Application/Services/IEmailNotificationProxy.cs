using SharedKernel.Dto.Features.School.User.Query;

namespace Module.Feedback.Application.Services;

public interface IEmailNotificationProxy
{
    Task SendNotificationAsync(IEnumerable<GetEmailsByUserIdsResponse> emailsTo, string emailFrom,
        Domain.Entities.Feedback feedback);
}