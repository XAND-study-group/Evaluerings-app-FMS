namespace SharedKernel.Dto.Features.Evaluering.Room.Command;

public record RemoveClassIdFromRoomRequest(
    Guid RoomId,
    Guid ClassId,
    byte[] RowVersion);