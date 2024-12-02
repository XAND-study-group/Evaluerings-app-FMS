using SharedKernel.Enums.Features.Vote;

namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetVoteResponse(
    Guid Id,
    byte[] RowVersion,
    string HashedId,
    VoteScale VoteScale)
{
    public GetVoteResponse() : this(default, Array.Empty<byte>(), string.Empty, default) { }
}