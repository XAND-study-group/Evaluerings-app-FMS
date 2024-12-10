using Module.Feedback.Domain.Entities;

namespace Module.Feedback.Application.Abstractions;

public interface IRoomRepository
{
    Task CreateRoomAsync(Room room);
    Task CreateRoomsAsync(IEnumerable<Room> rooms);

    Task<Room> GetRoomByIdAsync(Guid roomId);
    Task UpdateRoomAsync(Room room, byte[] rowVersion);
    Task DeleteRoomAsync(Room room, byte[] rowVersion);
}