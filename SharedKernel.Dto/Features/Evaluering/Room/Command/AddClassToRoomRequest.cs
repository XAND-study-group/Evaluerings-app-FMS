namespace SharedKernel.Dto.Features.Evaluering.Room.Command;

public record AddClassToRoomRequest(
    Guid RoomId,
    Guid ClassId,
    byte[] RowVersion);