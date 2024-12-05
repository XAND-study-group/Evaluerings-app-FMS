namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetSimpleRoomResponse(
    Guid RoomId,
    string Title,
    string Description);