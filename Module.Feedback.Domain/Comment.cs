using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Comment : Entity
{
    #region Properties

    public Guid UserId { get; protected set; }
    public Text CommentText { get; protected set; }
    public DateTime CreatedDateTime { get; init; }

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