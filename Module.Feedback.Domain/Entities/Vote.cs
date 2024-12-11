using Module.Feedback.Domain.ValueObjects;
using SharedKernel.Enums.Features.Vote;
using SharedKernel.Models;

namespace Module.Feedback.Domain.Entities;

public class Vote : Entity
{
    public HashedUserId HashedUserId { get; protected set; }
    public VoteScale VoteScale { get; protected set; }
    
    protected Vote()
    {
    }

    private Vote(HashedUserId hashedUserId, VoteScale voteScale)
    {
        HashedUserId = hashedUserId;
        VoteScale = voteScale;
    }
    
    #region Vote Methods

    internal static Vote Create(Guid userId, VoteScale voteScale) => new(userId, voteScale);

    internal void Update(Guid userId, VoteScale voteScale)
    {
        AssureUserHasVote(userId, HashedUserId);
        VoteScale = voteScale;
    }

    internal void Delete(Guid userId)
    {
        AssureUserHasVote(userId, HashedUserId);
    }

    private void AssureUserHasVote(Guid userId, HashedUserId hashedUserUserId)
    {
        if (userId != hashedUserUserId)
            throw new ArgumentException("Bruger skal være ejer af vote for at ændre den");
    }

    #endregion Vote Methods
}