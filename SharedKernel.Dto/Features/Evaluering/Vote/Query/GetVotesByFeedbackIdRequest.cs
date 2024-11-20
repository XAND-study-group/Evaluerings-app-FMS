namespace SharedKernel.Dto.Features.Evaluering.Vote.Query;

public record GetVotesByFeedbackIdRequest(
    Guid FeedbackId);