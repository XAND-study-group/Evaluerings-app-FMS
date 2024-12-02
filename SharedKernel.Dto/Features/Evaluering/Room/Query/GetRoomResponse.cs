namespace SharedKernel.Dto.Features.Evaluering.Room.Query;

public record GetRoomResponse(
    Guid RoomId,
    byte[] RowVersion,
    string Title,
    string Description)
{
    public GetRoomResponse() : this(default, Array.Empty<byte>(), string.Empty, string.Empty) { }
}