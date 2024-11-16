using SharedKernel.Enums.Features.Vote;
using SharedKernel.Interfaces.DomainServices;
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

    private Vote(Guid userId, VoteScale voteScale, IHashIdService hashIdService)
    {
        HashedId = new HashedId(userId, hashIdService);
        VoteScale = voteScale;
    }

    #endregion Constructors

    public static Vote Create(Guid userId, VoteScale voteScale, IHashIdService hashIdService)
    {
        var vote = new Vote(userId, voteScale, hashIdService);
        
        return vote;
    }
}