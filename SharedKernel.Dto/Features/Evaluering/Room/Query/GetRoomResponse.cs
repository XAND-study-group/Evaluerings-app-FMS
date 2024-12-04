namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetRoomResponse(
    Guid RoomId,
    byte[] RowVersion,
    string Title,
    string Description);