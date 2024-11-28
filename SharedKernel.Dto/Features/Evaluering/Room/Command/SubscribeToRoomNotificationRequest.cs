namespace SharedKernel.Dto.Features.Evaluering.Room.Command;

public record SubscribeToRoomNotificationRequest(
    Guid RoomId,
    Guid UserId,
    byte[] RowVersion);