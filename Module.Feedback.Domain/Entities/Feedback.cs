using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Domain.ValueObjects;
using SharedKernel.Enums.Features.Evaluering.Feedback;
using SharedKernel.Enums.Features.Vote;
using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain.Entities;

public class Feedback : Entity
{
    #region Properties

    // General Properties
    public HashedUserId HashedUserId { get; protected set; }
    public Title Title { get; protected set; }
    public Text Problem { get; protected set; }
    public Text Solution { get; protected set; }
    public DateTime Created { get; init; }
    public FeedbackState State { get; protected set; }
    public NotificationStatus NotificationStatus { get; protected set; }
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

    private Feedback(HashedUserId hashedUserUserId, string title, string problem, string solution, Room room)
    {
        HashedUserId = hashedUserUserId;
        Title = title;
        Problem = problem;
        Solution = solution;
        State = FeedbackState.Active;
        NotificationStatus = NotificationStatus.NotSent;
        Room = room;
        Created = DateTime.Now;
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

    public static Feedback CreateBogus(Guid userId, string title, string problem, string solution,
      Room room)
        =>  new Feedback(userId, title, problem, solution, room);    


    public void ChangeStatus(FeedbackState state)
    {
        State = state;
    }

    public int GetUpVoteCount()
    {
        return _votes.Count(vote => vote.VoteScale == VoteScale.UpVote);
    }

    public int GetDownVoteCount()
    {
        return _votes.Count(vote => vote.VoteScale == VoteScale.DownVote);
    }

    public int GetCommentsCount()
    {
        return _comments.Count + GetSubCommentsCount();
    }

    private int GetSubCommentsCount()
    {
        return _comments.Sum(comment => comment.SubComments.Count);
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

    public bool ShouldSendNotification()
    {
        if (State == FeedbackState.Solved ||
            NotificationStatus == NotificationStatus.Sent)
            return false;

        // This variable would normally be calculated from the total count of users associated to a room + a percentage number.
        var minimumsActivityCount = 1;

        var votesCount = Votes.Count;
        var commentsCount = Comments.Count;
        var subCommentsCount = Comments.Sum(c => c.SubComments.Count);

        var totalActivityCount = votesCount + commentsCount + subCommentsCount;

        return totalActivityCount >= minimumsActivityCount;
    }

    public void ChangeNotificationStatus(NotificationStatus status)
    {
        AssureStatusIsNotSolved();
        NotificationStatus = status;
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

    public Comment AddCommentBogus(Guid userId, string commentText)
    {
        AssureStatusIsNotSolved();

        var comment = Comment.CreateBogus(userId, commentText);
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
    public Comment AddSubCommentBogus(Guid commentId, Guid userId, string commentText)
    {
        AssureStatusIsNotSolved();

        var comment = GetCommentById(commentId);
        var subComment =  Comment.CreateBogus(userId, commentText);
        comment.AddSubComment(subComment);

        return subComment;
    }

    private Comment GetCommentById(Guid commentId)
    {
        return _comments.FirstOrDefault(c => c.Id == commentId) ??
               throw new ArgumentException("Kommentaren du prøver at kommentere kunne ikke findes");
    }

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

    public Vote DeleteVote(Guid voteId, Guid userId)
    {
        AssureStatusIsNotSolved();
        var vote = GetVoteById(voteId);
        vote.Delete(userId);
        return vote;
    }

    public Vote UpdateVote(Guid voteId, Guid userId, VoteScale voteScale)
    {
        AssureStatusIsNotSolved();

        var vote = GetVoteById(voteId);
        vote.Update(userId, voteScale);

        return vote;
    }

    private Vote GetVoteById(Guid voteId)
    {
        return _votes.FirstOrDefault(v => v.Id == voteId) ?? throw new ArgumentException("Votering kunne ikke findes");
    }

    #endregion Vote Related Methods

    #endregion Relational Methods


    #region Relational Business Logic Methods

    private void AssureNoExistingVoteFromUser(IEnumerable<Vote> votes, Guid userId)
    {
        if (votes.Any(v => v.HashedUserId == userId))
            throw new ArgumentException("Brugeren har allerede stemt på denne evaluering.");
    }

    private void AssureStatusIsNotSolved()
    {
        if (State == FeedbackState.Solved)
            throw new ArgumentException("Evalueringen er allerede løst.");
    }

    #endregion Relational Business Logic Methods
}