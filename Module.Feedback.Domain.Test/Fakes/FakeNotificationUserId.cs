using Module.Feedback.Domain.WrapperObjects;

namespace Module.Feedback.Domain.Test.Fakes;

public class FakeNotificationUserId : NotificationUserId
{
    public FakeNotificationUserId(Guid userId)
    {
        UserIdValue = userId;
    }
}