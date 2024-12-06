namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetFeedbackResponse(
    Guid Id,
    string HashedUserId,
    string Problem,
    string Solution,
    IEnumerable<Feedback.Query.GetCommentResponse> Comments,
    IEnumerable<Feedback.Query.GetVoteResponse> Votes,
    byte[] RowVersion);