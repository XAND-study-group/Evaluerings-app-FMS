using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain.Entities;

public class Comment : Entity
{
    #region Comment Methods

    internal static async Task<Comment> CreateAsync(Guid userId, string commentText,
        IValidationServiceProxy iIValidationServiceProxy)
    {
        await AssureAcceptableContent(commentText, iIValidationServiceProxy);

        var comment = new Comment(userId, commentText);

        return comment;
    }

    #endregion Comment Methods

    #region Comment Business Logic Methods

    private static async Task AssureAcceptableContent(string commentText,
        IValidationServiceProxy iIValidationServiceProxy)
    {
        var isAcceptable = await iIValidationServiceProxy.IsAcceptableCommentAsync(commentText);
        if (!isAcceptable.Valid)
            throw new ArgumentException(isAcceptable.Reason);
    }

    #endregion Comment Business Logic Methods

    #region Relational Methods

    public void AddSubComment(Comment comment)
    {
        _subComments.Add(comment);
    }

    #endregion Relational Methods

    #region Properties

    public Guid UserId { get; protected set; }
    public Text CommentText { get; protected set; }
    public DateTime Created { get; init; }

    private readonly List<Comment> _subComments = [];
    public IReadOnlyCollection<Comment> SubComments => _subComments;

    #endregion Properties

    #region Constructors

    protected Comment()
    {
    }

    private Comment(Guid userId, string commentText)
    {
        UserId = userId;
        CommentText = commentText;
    }

    #endregion Constructors
}