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
}