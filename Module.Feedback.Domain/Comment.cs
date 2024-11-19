using Module.Feedback.Domain.DomainServices;
using SharedKernel.Interfaces.DomainServices;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Comment : Entity
{
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

    #region Comment Methods

    public static async Task<Comment> CreateAsync(Guid userId, string commentText, IValidationServiceProxy iIValidationServiceProxy)
    {
        var comment = new Comment(userId, commentText);

        await AssureAcceptableContent(commentText, iIValidationServiceProxy);

        return comment;
    }

    #endregion Comment Methods

    #region Comment Business Logic Methods

    private static async Task AssureAcceptableContent(string commentText, IValidationServiceProxy iIValidationServiceProxy)
    {
        var isAcceptable = await iIValidationServiceProxy.IsAcceptableContentAsync(commentText);
        if (!isAcceptable)
            throw new ArgumentException("Comment is not acceptable");
    }

    #endregion Comment Business Logic Methods

    #region Relational Methods

    public void AddSubComment(Comment comment)
    {

        _subComments.Add(comment);
    }

    #endregion Relational Methods
}