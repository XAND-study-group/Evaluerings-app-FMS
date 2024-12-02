using SharedKernel.Dto.Features.Evaluering.Room.Query;

namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetAllFeedbacksResponse(
    Guid Id,
    byte[] RowVersion,
    string HashedId,
    string Title,
    string Problem,
    string Solution,
    IEnumerable<GetCommentResponse> Comments,
    IEnumerable<GetVoteResponse> Votes)
{
    public GetAllFeedbacksResponse() : this(default, Array.Empty<byte>(), string.Empty, string.Empty, string.Empty, string.Empty, Array.Empty<GetCommentResponse>(), Array.Empty<GetVoteResponse>()) { }
}