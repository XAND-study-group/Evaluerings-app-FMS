﻿using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.Entities;
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
    {
        return await feedbackDbContext.Rooms
                   .Include(r => r.ClassIds)
                   .Include(r => r.NotificationSubscribedUserIds)
                   .FirstOrDefaultAsync(r => r.Id == roomId) ??
               throw new ArgumentException("Room not found");
    }

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

    async Task IRoomRepository.CreateRoomsAsync(IEnumerable<Room> rooms)
    {
        await feedbackDbContext.AddRangeAsync(rooms);
        await feedbackDbContext.SaveChangesAsync();
    }
}