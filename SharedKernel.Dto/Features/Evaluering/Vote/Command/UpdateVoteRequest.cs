using SharedKernel.Enums.Features.Vote;

namespace SharedKernel.Dto.Features.Evaluering.Vote.Command;

public record UpdateVoteRequest(
    Guid FeedbackId,
    Guid VoteId,
    Guid UserId,
    VoteScale VoteScale,
    byte[] RowVersion);