namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetFeedbackResponse(
    Guid FeedbackId,
    string HashedId,
    string Problem,
    string Solution,
    IEnumerable<GetCommentResponse> Comments,
    IEnumerable<GetVoteResponse> Votes)
{
    public GetFeedbackResponse() : this(default, string.Empty, string.Empty, string.Empty, Array.Empty<GetCommentResponse>(), Array.Empty<GetVoteResponse>()) { }
}