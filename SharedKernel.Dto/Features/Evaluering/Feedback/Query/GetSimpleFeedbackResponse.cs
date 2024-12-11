namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetSimpleFeedbackResponse(
    Guid Id,
    byte[] RowVersion,
    string HashedUserId,
    string Title,
    string Problem,
    string Solution);