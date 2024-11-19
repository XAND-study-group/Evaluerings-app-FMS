namespace SharedKernel.Dto.Features.Evaluering.Vote.Command;

public record DeleteVoteRequest(
    Guid VoteId,
    byte[] RowVersion);