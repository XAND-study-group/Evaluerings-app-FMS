namespace SharedKernel.Dto.Features.Evaluering.Feedback.Command;

public record DeleteFeedbackRequest(
    Guid FeedbackId,
    byte[] RowVersion);