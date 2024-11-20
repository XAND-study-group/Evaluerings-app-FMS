namespace SharedKernel.Dto.Features.Evaluering.Comment.Command;

public record CreateSubCommentRequest(
    Guid CommentId,
    Guid UserId,
    string CommentText);