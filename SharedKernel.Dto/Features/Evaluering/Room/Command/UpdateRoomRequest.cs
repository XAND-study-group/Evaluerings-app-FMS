namespace SharedKernel.Dto.Features.Evaluering.Room.Command;

public record UpdateRoomRequest(
    Guid RoomId,
    byte[] RowVersion,
    string Title,
    string Description);