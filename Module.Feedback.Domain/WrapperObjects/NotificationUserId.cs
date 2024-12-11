using SharedKernel.Models;

namespace Module.Feedback.Domain.WrapperObjects;

public class NotificationUserId : Entity
{
    protected NotificationUserId()
    {
    }

    private NotificationUserId(Guid userId)
    {
        UserIdValue = userId;
    }

    public Guid UserIdValue { get; protected set; }

    public static NotificationUserId Create(Guid userId)
    {
        return new NotificationUserId(userId);
    }
}