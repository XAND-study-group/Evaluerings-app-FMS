namespace Module.Feedback.Domain;

public class Feedback
{
    public static Feedback Create(Guid userId, string problem, string solution)
        => new Feedback();
}