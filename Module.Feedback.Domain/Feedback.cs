using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Domain.ValueObjects;
using SharedKernel.Enums.Features.Evaluering.Feedback;
using SharedKernel.Enums.Features.Vote;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Feedback : Entity
{
    #region Properties

    // General Properties
    public HashedId HashedUserId { get; protected set; }
    public Title Title { get; protected set; }
    public Text Problem { get; protected set; }
    public Text Solution { get; protected set; }
    public DateTime Created { get; init; }
    public FeedbackStatus Status { get; protected set; }
    public Room Room { get; protected set; }

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

    private Feedback(HashedId hashedUserUserId, string title, string problem, string solution, Room room)
    {
        HashedUserId = hashedUserUserId;
        Title = title;
        Problem = problem;
        Solution = solution;
        Status = FeedbackStatus.Active;
        Room = room;
    }

    #endregion Constructors

    #region Feedback Methods

    public static async Task<Feedback> CreateAsync(Guid userId, string title, string problem, string solution,
        Room room, IValidationServiceProxy iIValidationServiceProxy)
    {
        var feedback = new Feedback(userId, title, problem, solution, room);

        await AiTextCheckAsync(feedback.Title, feedback.Problem, feedback.Solution, iIValidationServiceProxy);

        return feedback;
    }

    #endregion Feedback Methods

    #region Feedback Business Logic Methods

    private static async Task AiTextCheckAsync(string title, string problem, string solution,
        IValidationServiceProxy iValidationServiceProxy)
    {
        var isAcceptable = await iValidationServiceProxy.IsAcceptableContentAsync(title, problem, solution);
        if (!isAcceptable.Valid)
            throw new ArgumentException(isAcceptable.Reason);
    }

    #endregion Feedback Business Logic Methods

    #region Relational Methods

    #region Comment Related Methods

    public async Task<Comment> AddComment(Guid userId, string commentText,
        IValidationServiceProxy validationServiceProxy)
    {
        AssureStatusIsNotSolved();
        
        var comment = await Comment.CreateAsync(userId, commentText, validationServiceProxy);
        _comments.Add(comment);

        return comment;
    }

    public async Task<Comment> AddSubCommentAsync(Guid commentId, Guid userId, string commentText,
        IValidationServiceProxy validationServiceProxy)
    {
        AssureStatusIsNotSolved();

        var comment = GetCommentById(commentId);
        var subComment = await Comment.CreateAsync(userId, commentText, validationServiceProxy);
        comment.AddSubComment(subComment);

        return subComment;
    }

    private Comment GetCommentById(Guid commentId)
        => _comments.FirstOrDefault(c => c.Id == commentId) ??
           throw new ArgumentException("Kommentaren du prøver at kommentere kunne ikke findes");

    #endregion Comment Related Methods

    #region Vote Related Methods

    public Vote AddVote(Guid userId, VoteScale voteScale)
    {
        AssureStatusIsNotSolved();
        AssureNoExistingVoteFromUser(_votes, userId);

        var vote = Vote.Create(userId, voteScale);

        _votes.Add(vote);

        return vote;
    }

    public Vote DeleteVote(Guid voteId)
    {
        AssureStatusIsNotSolved();

        return GetVoteById(voteId);
    }

    public Vote UpdateVote(Guid voteId, VoteScale voteScale)
    {
        AssureStatusIsNotSolved();

        var vote = GetVoteById(voteId);
        vote.Update(voteScale);

        return vote;
    }

    private Vote GetVoteById(Guid voteId)
        => _votes.FirstOrDefault(v => v.Id == voteId) ?? throw new ArgumentException("Vote kunne ikke findes");

    #endregion Vote Related Methods

    #endregion Relational Methods


    #region Relational Business Logic Methods

    private void AssureNoExistingVoteFromUser(IEnumerable<Vote> votes, Guid userId)
    {
        if (votes.Any(v => v.HashedUserId == userId))
            throw new ArgumentException("User has already voted for this feedback.");
    }

    private void AssureStatusIsNotSolved()
    {
        if (Status == FeedbackStatus.Solved)
            throw new ArgumentException("Evalueringen er allerede løst.");
    }

    #endregion Relational Business Logic Methods
}