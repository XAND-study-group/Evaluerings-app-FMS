using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.Models;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain.Entities;

public class Comment : Entity
{
    public Guid UserId { get; }
    public Text CommentText { get; protected set; }
    public DateTime Created { get; init; }

    private readonly List<Comment> _subComments = [];
    public IReadOnlyCollection<Comment> SubComments => _subComments;
    
    protected Comment()
    {
    }

    private Comment(Guid userId, string commentText)
    {
        UserId = userId;
        CommentText = commentText;
        Created = DateTime.Now;
    }
    
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

    #region Comment Methods

    internal static async Task<Comment> CreateAsync(Guid userId, string commentText,
        IValidationServiceProxy iIValidationServiceProxy)
    {
        await AssureAcceptableContent(commentText, iIValidationServiceProxy);

        var comment = new Comment(userId, commentText);

        return comment;
    }

    internal static Comment CreateBogus(Guid userId, string commentText)
    {
        var comment = new Comment(userId, commentText);
        return comment;
    }

    #endregion Comment Methods
}