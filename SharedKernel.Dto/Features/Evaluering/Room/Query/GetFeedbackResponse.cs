using SharedKernel.Dto.Features.Evaluering.Feedback.Query;

namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetFeedbackResponse(
    Guid Id,
    string HashedUserId,
    string Problem,
    string Solution,
    IEnumerable<GetCommentResponse> Comments,
    IEnumerable<GetDetailedVoteResponse> Votes,
    byte[] RowVersion);