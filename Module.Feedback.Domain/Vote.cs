using SharedKernel.Enums.Features.Vote;
using SharedKernel.Interfaces.DomainServices;
using SharedKernel.Interfaces.DomainServices.Interfaces;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Domain;

public class Vote : Entity
{
    #region Properties

    public HashedId HashedId { get; protected set; }
    public VoteScale VoteScale { get; protected set; }

    #endregion Properties

    #region Constructors

    protected Vote()
    {
    }

    private Vote(HashedId hashedId, VoteScale voteScale)
    {
        HashedId = hashedId;
        VoteScale = voteScale;
    }

    #endregion Constructors

    #region Vote Methods

    public static Vote Create(Guid userId, VoteScale voteScale, IHashIdService hashIdService)
    {
        var hashedId = HashedId.Create(userId, hashIdService);
        var vote = new Vote(hashedId, voteScale);

        return vote;
    }

    public void Update(VoteScale voteScale)
    {
        VoteScale = voteScale;
    }

    #endregion Vote Methods
}