using SharedKernel.Interfaces.DomainServices;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Room : Entity
{
    #region Properties

    public Title Title { get; protected set; }
    public Text Description { get; protected set; }
    private readonly List<Feedback> _feedbacks = [];
    private readonly List<Guid> _classIds = [];
    public IReadOnlyCollection<Feedback> Feedbacks => _feedbacks;
    public IReadOnlyCollection<Guid> ClassIds => _classIds;

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

    public async Task<Feedback> AddFeedbackAsync(Guid userId, string title, string problem, string solution,
        IHashIdService hashIdService, IFeedbackAiService feedbackAiService)
    {
        var feedback = await Feedback.CreateAsync(userId, title, problem, solution, hashIdService, feedbackAiService);

        _feedbacks.Add(feedback);

        return feedback;
    }

    public async Task AddClassIdAsync(Guid classId)
    {
        _classIds.Add(classId);
    }

    #endregion Relational Methods
}