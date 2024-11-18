using SharedKernel.Dto.Features.Evaluering.Feedback.Query;

namespace Module.Feedback.Domain.Extensions;

public static class VoteExtension
{
    public static GetVoteResponse MapToGetVoteResponse(this Vote vote)
    {
        return new GetVoteResponse(vote.Id, vote.RowVersion, vote.HashedId, vote.VoteScale);
    }
    
    public static IEnumerable<GetVoteResponse> MapToIEnumerableGetVoteResponse(this IEnumerable<Vote> votes)
    {
        return votes.Select(vote => vote.MapToGetVoteResponse());
    }
}