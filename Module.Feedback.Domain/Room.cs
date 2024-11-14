using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Room : Entity
{
    #region Properties

    public Title Title { get; protected set; }
    public Text Description { get; protected set; }
    private readonly List<Feedback> _feedbacks = [];
    public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks;

    #endregion Properties

    #region Constructors

    protected Room()
    {
    }

    private Room(string title, string description)
    {
        Title = title;
        Description = description;
    }

    #endregion Constructors

    #region Room Methods

    public static Room Create(string title, string description)
        => new Room(title, description);

    public void Update(string title, string description)
    {
        Title = title;
        Description = description;
    }

    #endregion Room Methods
    
    #region Relational Methods

    public Feedback AddFeedback(Guid userId, string problem, string solution)
    {
        var feedback = Feedback.Create(userId, problem, solution);
        
        _feedbacks.Add(feedback);

        return feedback;
    }
    #endregion Relational Methods
}