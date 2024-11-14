namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetCommentResponse(
    Guid CommentId,
    string CommentText);