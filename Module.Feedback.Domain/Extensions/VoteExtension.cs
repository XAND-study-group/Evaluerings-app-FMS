using SharedKernel.Dto.Features.Evaluering.Feedback.Query;

namespace Module.Feedback.Domain.Extensions;

public static class VoteExtension
{
    public static GetVoteResponse MapToGetVoteResponse(this Vote vote)
    {
        return new GetVoteResponse(vote.Id, vote.RowVersion, vote.HashedId, vote.VoteScale);
    }
}