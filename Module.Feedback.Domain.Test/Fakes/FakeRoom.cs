namespace Module.Feedback.Domain.Test.Fakes;

public class FakeRoom : Room
{
    public FakeRoom()
    {
    }

    public FakeRoom(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public void SetTitle(string title)
        => Title = title;

    public void SetDescription(string description)
        => Description = description;

    public new void AssureNoDuplicateClassIds(Guid classId, IEnumerable<Guid> currentClassIds)
        => base.AssureNoDuplicateClassIds(classId, currentClassIds);

    public new void AssureClassIdIsInList(Guid classId, IEnumerable<Guid> currentClassIds)
        => base.AssureClassIdIsInList(classId, currentClassIds);

    public new void AssureNoDuplicateUserIds(Guid userId, IEnumerable<Guid> currentUserIds)
        => base.AssureNoDuplicateUserIds(userId, currentUserIds);

    public new void AssureUserIdIsInList(Guid userId, IEnumerable<Guid> currentUserIds)
        => base.AssureUserIdIsInList(userId, currentUserIds);

    public FakeRoom CreateRoom(string title, string description)
    {
        return new FakeRoom
        {
            Title = title,
            Description = description
        };
    }
    public new void AddFeedback(Feedback feedback)
    => base.AddFeedback(feedback);

    public new void AddClassId(Guid classId)
        => base.AddClassId(classId);

}