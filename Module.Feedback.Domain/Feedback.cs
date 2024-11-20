using Module.Feedback.Domain.DomainServices;
using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.Enums.Features.Vote;
using SharedKernel.Interfaces.DomainServices;
using SharedKernel.Interfaces.DomainServices.Interfaces;
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

    private Feedback(HashedId hashedUserId, string title, string problem, string solution)
    {
        HashedId = hashedUserId;
        Title = title;
        Problem = problem;
        Solution = solution;
    }

    #endregion Constructors

    #region Feedback Methods

    public static async Task<Feedback> CreateAsync(Guid userId, string title, string problem, string solution,
        IHashIdService hashIdService, IValidationServiceProxy iIValidationServiceProxy)
    {
        var hashedUserId = HashedId.Create(userId, hashIdService);
        var feedback = new Feedback(hashedUserId, title, problem, solution);
        
        await AiTextCheckAsync(feedback.Title, feedback.Problem, feedback.Solution, iIValidationServiceProxy);

        return feedback;
    }

    #endregion Feedback Methods

    #region Feedback Business Logic Methods
    private static async Task AiTextCheckAsync(string title, string problem, string solution, IValidationServiceProxy iValidationServiceProxy)
    {
        var isAcceptable = await iValidationServiceProxy.IsAcceptableContentAsync(title, problem, solution);
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