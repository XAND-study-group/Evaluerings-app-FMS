namespace SharedKernel.Dto.Features.Evaluering.Feedback.Command;

public record CreateFeedbackRequest(
    Guid UserId,
    string Title,
    string Problem,
    string Solution,
    Guid RoomId);