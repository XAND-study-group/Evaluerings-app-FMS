using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain;
using Module.Feedback.Infrastructure.DbContexts;

namespace Module.Feedback.Infrastructure.Repositories;

public class RoomRepository(FeedbackDbContext feedbackDbContext) : IRoomRepository
{
    async Task IRoomRepository.CreateRoomAsync(Room room)
    {
        await feedbackDbContext.Rooms.AddAsync(room);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task<Room> IRoomRepository.GetRoomByIdAsync(Guid roomId)
        => await feedbackDbContext.Rooms
            .Include(r => r.ClassIds)
            .SingleAsync(r => r.Id == roomId);

    async Task IRoomRepository.UpdateRoomAsync(Room room, byte[] rowVersion)
    {
        feedbackDbContext.Entry(room).Property(nameof(Room.RowVersion)).OriginalValue = rowVersion;
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task IRoomRepository.DeleteRoomAsync(Room room, byte[] rowVersion)
    {
        feedbackDbContext.Entry(room).Property(nameof(Room.RowVersion)).OriginalValue = rowVersion;
        feedbackDbContext.Rooms.Remove(room);
        await feedbackDbContext.SaveChangesAsync();
    }
}