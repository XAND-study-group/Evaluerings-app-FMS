namespace SharedKernel.Dto.Features.Evaluering.Comment.Command;

public record CreateCommentRequest(
    Guid FeedbackId,
    Guid UserId,
    string CommentText);