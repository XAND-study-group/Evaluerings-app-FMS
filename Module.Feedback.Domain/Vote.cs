using System.Reflection.Metadata.Ecma335;
using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Domain.ValueObjects;
using SharedKernel.Enums.Features.Vote;

namespace Module.Feedback.Domain;

public class Vote : Entity
{
    #region Properties

    public HashedUserId HashedUserId { get; protected set; }
    public VoteScale VoteScale { get; protected set; }

    #endregion Properties

    #region Constructors

    protected Vote()
    {
    }

    private Vote(HashedUserId hashedUserId, VoteScale voteScale)
    {
        HashedUserId = hashedUserId;
        VoteScale = voteScale;
    }

    #endregion Constructors

    #region Vote Methods

    public static Vote Create(Guid userId, VoteScale voteScale)
        => new(userId, voteScale);

    public void Update(Guid userId, VoteScale voteScale)
    {
        AssureUserHasVote(userId, HashedUserId);
        VoteScale = voteScale;
    }

    private void AssureUserHasVote(Guid userId, HashedUserId hashedUserUserId)
    {
        if (userId != hashedUserUserId) 
            throw new ArgumentException("Bruger skal være ejer af vote for at ændre den");
    }

    #endregion Vote Methods
}