﻿using SharedKernel.Enums.Features.Vote;
using SharedKernel.Interfaces.DomainServices;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain.Test.Fakes;

public class FakeVote : Vote
{
    public FakeVote()
    {
        
    }
    
    public FakeVote(VoteScale voteScale)
    {
        VoteScale = voteScale;
    }
    
    public void SetHashId(Guid userId, IHashIdService hashIdService)
    => HashedId = new HashedId(userId, hashIdService);
}