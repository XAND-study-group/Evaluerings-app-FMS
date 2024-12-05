namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetDetailedRoomResponse(
    Guid RoomId,
    byte[] RowVersion,
    string Title,
    string Description);