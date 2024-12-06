namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetCommentResponse(
    Guid Id,
    string CommentText,
    DateTime Created,
    byte[] RowVersion);