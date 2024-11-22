namespace SharedKernel.Dto.Features.Evaluering.Comment.Command;

public record CreateSubCommentRequest(
    Guid FeedbackId,
    Guid CommentId,
    Guid UserId,
    string CommentText);