using SharedKernel.Enums.Features.Evaluering.Feedback;

namespace SharedKernel.Dto.Features.Evaluering.Feedback.Command;

public record ChangeFeedbackStatusRequest(
    Guid FeedbackId,
    FeedbackState State,
    byte[] RowVersion);