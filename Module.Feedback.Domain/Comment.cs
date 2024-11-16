using SharedKernel.Interfaces.DomainServices;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Comment : Entity
{
    #region Properties

    public Guid UserId { get; protected set; }
    public Text CommentText { get; protected set; }
    public DateTime CreatedDateTime { get; init; }
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

    public static Comment Create(Guid userId, string commentText, IFeedbackAiService feedbackAiService)
    {
        var comment = new Comment(userId, commentText);
        
        return comment;
    }
}