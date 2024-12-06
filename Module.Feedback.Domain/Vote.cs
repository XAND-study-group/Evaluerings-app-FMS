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

    internal static Vote Create(Guid userId, VoteScale voteScale)
        => new(userId, voteScale);

    internal void Update(VoteScale voteScale)
        => VoteScale = voteScale;

    #endregion Vote Methods
}