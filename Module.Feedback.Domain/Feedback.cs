using SharedKernel.Interfaces.DomainServices;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Feedback : Entity
{
    #region Properties

    // General Properties
    public HashedId HashedId { get; protected set; }
    public Title Title { get; protected set; }
    public Text Problem { get; protected set; }
    public Text Solution { get; protected set; }
    public DateTime CreatedDateTime { get; init; }

    // List Properties
    private readonly List<Comment> _comments = [];
    private readonly List<Vote> _votes = [];
    public IReadOnlyCollection<Comment> Comments => _comments;
    public IReadOnlyCollection<Vote> Votes => _votes;

    #endregion Properties

    #region Constructors

    protected Feedback(){}
    
    private Feedback(Guid userId, string title, string problem, string solution, IHashIdService hashIdService)
    {
        HashedId = new HashedId(userId, hashIdService);
        Title = title;
        Problem = problem;
        Solution = solution;
    }

    #endregion Constructors

    #region Feedback Methods
    public static async Task<Feedback> CreateAsync(Guid userId, string title, string problem, string solution,
        IHashIdService hashIdService, IFeedbackAiService feedbackAiService)
    {
        var feedback = new Feedback(userId, title, problem, solution, hashIdService);

        await AiTitleCheckAsync(feedback.Title, feedbackAiService);
        await AiTextCheckAsync(feedback.Problem, feedbackAiService);
        await AiTextCheckAsync(feedback.Solution, feedbackAiService);
        
        return feedback;
    }
    
    #endregion Feedback Methods
    
    #region Feedback Business Logic Methods

    private static async Task AiTitleCheckAsync(string title, IFeedbackAiService feedbackAiService)
    {
        var isAcceptable = await feedbackAiService.IsAcceptableTitleAsync(title);
        if (!isAcceptable)
            throw new ArgumentException("Title is not acceptable");
    }
    
    private static async Task AiTextCheckAsync(string text, IFeedbackAiService feedbackAiService)
    {
        var isAcceptable = await feedbackAiService.IsAcceptableContentAsync(text);
        if (!isAcceptable)
            throw new ArgumentException("The value is not acceptable.");
    }
    
    #endregion Feedback Business Logic Methods
}