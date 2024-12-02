namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetAllRoomsResponse(
    Guid RoomId,
    string Title,
    string Description)
{
    public GetAllRoomsResponse() : this(default, string.Empty, string.Empty) { }
}