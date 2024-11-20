namespace SharedKernel.Dto.Features.Evaluering.Feedback.Query;

public record GetFeedbacksByRoomIdRequest(
    Guid RoomId);