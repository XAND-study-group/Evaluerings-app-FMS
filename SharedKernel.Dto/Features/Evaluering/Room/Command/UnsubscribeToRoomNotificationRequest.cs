namespace SharedKernel.Dto.Features.Evaluering.Room.Command;

public record UnsubscribeToRoomNotificationRequest(
    Guid RoomId,
    Guid UserId,
    byte[] RowVersion);