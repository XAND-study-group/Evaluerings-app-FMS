namespace Module.Feedback.Domain.Test.Fakes;

public class FakeComment : Comment
{
    public FakeComment()
    {
    }

    public void SetCommentText(string commentText)
        => CommentText = commentText;

}