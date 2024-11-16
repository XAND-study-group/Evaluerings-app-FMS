namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetCommentResponse(
    Guid UserId,
    string CommentText);