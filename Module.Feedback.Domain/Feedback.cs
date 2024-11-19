using Module.Feedback.Domain.DomainServices;
using SharedKernel.Enums.Features.Vote;
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

    protected Feedback()
    {
    }

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
        IHashIdService hashIdService, IValidationServiceProxy iIValidationServiceProxy)
    {
        var feedback = new Feedback(userId, title, problem, solution, hashIdService);

        await AiTitleCheckAsync(feedback.Title, iIValidationServiceProxy);
        await AiTextCheckAsync(feedback.Problem, iIValidationServiceProxy);
        await AiTextCheckAsync(feedback.Solution, iIValidationServiceProxy);

        return feedback;
    }

    #endregion Feedback Methods

    #region Feedback Business Logic Methods

    private static async Task AiTitleCheckAsync(string title, IValidationServiceProxy iValidationServiceProxy)
    {
        var isAcceptable = await iValidationServiceProxy.IsAcceptableTitleAsync(title);
        if (!isAcceptable.Valid)
            throw new ArgumentException(isAcceptable.Reason);
    }

    private static async Task AiTextCheckAsync(string text, IValidationServiceProxy iValidationServiceProxy)
    {
        var isAcceptable = await iValidationServiceProxy.IsAcceptableContentAsync(text);
        if (!isAcceptable.Valid)
            throw new ArgumentException(isAcceptable.Reason);
    }

    #endregion Feedback Business Logic Methods

    #region Relational Methods

    public async Task<Comment> AddComment(Guid userId, string commentText, IValidationServiceProxy iValidationServiceProxy)
    {
        var comment = await Comment.CreateAsync(userId, commentText, iValidationServiceProxy);

        _comments.Add(comment);

        return comment;
    }

    public Vote AddVote(Guid userId, VoteScale voteScale, IHashIdService hashIdService)
    {
        var vote = Vote.Create(userId, voteScale, hashIdService);

        _votes.Add(vote);

        return vote;
    }

    #endregion Relational Methods
}