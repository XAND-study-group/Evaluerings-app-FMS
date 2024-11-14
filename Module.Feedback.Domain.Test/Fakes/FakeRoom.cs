namespace Module.Feedback.Domain.Test.Fakes;

public class FakeRoom : Room
{
    public FakeRoom() : base()
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
}