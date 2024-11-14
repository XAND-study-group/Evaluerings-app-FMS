namespace Module.Feedback.Domain;

public class Feedback
{
    private readonly List<Comment> _comments = [];
    public IReadOnlyCollection<Comment> Comments => _comments;
    private readonly List<Vote> _votes = [];
    public IReadOnlyCollection<Vote> Votes => _votes;
    public static Feedback Create(Guid userId, string problem, string solution)
        => new Feedback();
}