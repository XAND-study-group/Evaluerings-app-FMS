using SharedKernel.Enums.Features.Vote;

namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetVoteResponse(
    string HashedId,
    VoteScale VoteScale);