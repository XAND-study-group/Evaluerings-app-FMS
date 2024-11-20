namespace SharedKernel.Dto.Features.Evaluering.Room.Command;

public record DeleteRoomRequest(
    Guid RoomId,
    byte[] RowVersion);