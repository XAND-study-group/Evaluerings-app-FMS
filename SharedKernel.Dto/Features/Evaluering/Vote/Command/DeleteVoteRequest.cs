namespace SharedKernel.Dto.Features.Evaluering.Vote.Command;

public record DeleteVoteRequest(
    Guid FeedbackId,
    Guid VoteId,
    Guid UserId,
    byte[] RowVersion);