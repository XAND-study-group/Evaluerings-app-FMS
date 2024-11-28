using Module.Feedback.Domain.DomainServices.Interfaces;
using Module.Feedback.Domain.ValueObjects;
using SharedKernel.Enums.Features.Vote;

namespace Module.Feedback.Domain;

public class Vote : Entity
{
    #region Properties

    public HashedId HashedUserId { get; protected set; }
    public VoteScale VoteScale { get; protected set; }

    #endregion Properties

    #region Constructors

    protected Vote()
    {
    }

    private Vote(HashedId hashedUserId, VoteScale voteScale)
    {
        HashedUserId = hashedUserId;
        VoteScale = voteScale;
    }

    #endregion Constructors

    #region Vote Methods

    public static Vote Create(Guid userId, VoteScale voteScale)
        => new(userId, voteScale);

    public void Update(VoteScale voteScale)
        => VoteScale = voteScale;

    #endregion Vote Methods
}