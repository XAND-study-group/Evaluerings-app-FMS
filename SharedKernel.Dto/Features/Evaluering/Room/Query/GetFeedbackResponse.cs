namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetFeedbackResponse(
    Guid FeedbackId,
    string HashedUserId,
    string Problem,
    string Solution,
    IEnumerable<GetCommentResponse> Comments,
    IEnumerable<GetVoteResponse> Votes);
