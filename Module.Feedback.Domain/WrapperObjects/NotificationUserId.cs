using Module.Feedback.Domain.Entities;
using SharedKernel.Models;

namespace Module.Feedback.Domain.WrapperObjects;

public class NotificationUserId : Entity
{
    public Guid UserIdValue { get; protected set; }

    protected NotificationUserId() { }
    
    private NotificationUserId(Guid userId)
    {
        UserIdValue = userId;
    }

    public static NotificationUserId Create(Guid userId)
        => new (userId);
}