using Module.Feedback.Domain.Entities;

namespace Module.Feedback.Domain.Test.Fakes;

public class FakeComment : Comment
{
    public void SetCommentText(string commentText)
    {
        CommentText = commentText;
    }
}