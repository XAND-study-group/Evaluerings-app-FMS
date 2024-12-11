using SharedKernel.Enums.Features.Vote;

namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetDetailedVoteResponse(
    Guid Id,
    byte[] RowVersion,
    string HashedUserId,
    VoteScale VoteScale);