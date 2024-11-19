using SharedKernel.Dto.Features.Evaluering.Feedback.Query;

namespace Module.Feedback.Domain.Extensions;

public static class VoteExtension
{
    public static GetVoteResponse MapToGetVoteResponse(this Vote vote) =>
        new(vote.Id, vote.RowVersion, vote.HashedId, vote.VoteScale);
    
    public static IEnumerable<GetVoteResponse> MapToIEnumerableGetVoteResponse(this IEnumerable<Vote> votes)
        => votes.Select(vote => vote.MapToGetVoteResponse());
}