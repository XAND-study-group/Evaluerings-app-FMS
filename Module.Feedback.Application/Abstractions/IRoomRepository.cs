using Module.Feedback.Domain;

namespace Module.Feedback.Application.Abstractions;

public interface IRoomRepository
{
    Task CreateRoomAsync(Room room);
    Task<Room> GetRoomByIdAsync(Guid roomId);
    Task UpdateRoomAsync(Room room, byte[] rowVersion);
    Task DeleteRoomAsync(Room room, byte[] rowVersion);
}