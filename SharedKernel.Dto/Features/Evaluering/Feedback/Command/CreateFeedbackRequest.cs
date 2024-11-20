namespace SharedKernel.Dto.Features.Evaluering.Feedback.Command;

public record CreateFeedbackRequest(
    Guid roomId,
    Guid userId,
    string title,
    string problem,
    string solution);