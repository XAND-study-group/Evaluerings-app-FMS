using SharedKernel.Dto.Features.Evaluering.Room.Query;

namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetAllFeedbacksResponse(
    Guid FeedbackId,
    byte[] RowVersion,
    string HashedId,
    string Title,
    string Problem,
    string Solution,
    IEnumerable<GetCommentResponse> Comments,
    IEnumerable<GetVoteResponse> Votes);