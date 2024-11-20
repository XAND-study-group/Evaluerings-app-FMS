using SharedKernel.Enums.Features.Vote;

namespace SharedKernel.Dto.Features.Evaluering.Vote.Command;

public record CreateVoteRequest(
    Guid RoomId,
    Guid FeedbackId,
    Guid UserId,
    VoteScale VoteScale);