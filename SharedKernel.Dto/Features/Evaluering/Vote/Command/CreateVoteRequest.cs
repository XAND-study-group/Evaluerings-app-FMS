using SharedKernel.Enums.Features.Vote;

namespace SharedKernel.Dto.Features.Evaluering.Vote.Command;

public record CreateVoteRequest(
    Guid FeedbackId,
    Guid UserId,
    VoteScale VoteScale);