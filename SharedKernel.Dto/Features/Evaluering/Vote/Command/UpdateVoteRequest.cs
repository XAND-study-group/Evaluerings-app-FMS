using SharedKernel.Enums.Features.Vote;

namespace SharedKernel.Dto.Features.Evaluering.Vote.Command;

public record UpdateVoteRequest(
    Guid VoteId,
    VoteScale VoteScale,
    byte[] RowVersion);