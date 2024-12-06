namespace Module.Feedback.Application.Services;

public interface IEmailNotificationProxy
{
    Task SendNotificationAsync(IEnumerable<string> emailsTo, string emailFrom, Domain.Entities.Feedback feedback);
}