using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Domain.ValueObjects;
using SharedKernel.Enums.Features.Vote;

namespace Module.Feedback.Domain.Test.Fakes;

public class FakeVote : Vote
{
    public FakeVote()
    {
        
    }
    
    public FakeVote(Guid userId, VoteScale voteScale)
    {
        HashedUserId = userId;
        VoteScale = voteScale;
    }
    
    public void SetHashId(Guid userId)
    => HashedUserId = userId;
}