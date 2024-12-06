namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetAllFeedbacksResponse(
    Guid Id,
    byte[] RowVersion,
    string HashedUserId,
    string Title,
    string Problem,
    string Solution,
    IEnumerable<GetCommentResponse> Comments,
    IEnumerable<GetVoteResponse> Votes);