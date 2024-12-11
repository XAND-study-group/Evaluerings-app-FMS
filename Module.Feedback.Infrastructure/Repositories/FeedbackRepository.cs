using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.Entities;
using Module.Feedback.Infrastructure.DbContexts;

namespace Module.Feedback.Infrastructure.Repositories;

public class FeedbackRepository(FeedbackDbContext feedbackDbContext) : IFeedbackRepository
{
    public async Task<Room> GetRoomByIAsync(Guid roomId)
    {
        return await feedbackDbContext.Rooms
                   .Include(r => r.ClassIds)
                   .FirstOrDefaultAsync(r => r.Id == roomId) ??
               throw new ArgumentException("Room not found");
    }

    async Task IFeedbackRepository.CreateFeedbackAsync(Domain.Entities.Feedback feedback)
    {
        await feedbackDbContext.Feedbacks.AddAsync(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }

     async Task<Domain.Entities.Feedback> IFeedbackRepository.GetFeedbackByIdAsync(Guid feedbackId)
    {
        return await feedbackDbContext.Feedbacks
                   .Include(f => f.Room)
                   .ThenInclude(r => r.ClassIds)
                   .FirstOrDefaultAsync(f => f.Id == feedbackId) ??
               throw new ArgumentException("Feedback not found");
    }

    async Task IFeedbackRepository.DeleteFeedbackAsync(Domain.Entities.Feedback feedback, byte[] rowVersion)
    {
        feedbackDbContext.Entry(feedback).Property(nameof(Domain.Entities.Feedback.RowVersion)).OriginalValue =
            rowVersion;
        feedbackDbContext.Feedbacks.Remove(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task IFeedbackRepository.UpdateFeedbackAsync(Domain.Entities.Feedback feedback, byte[] rowVersion)
    {
        feedbackDbContext.Entry(feedback).Property(nameof(Domain.Entities.Feedback.RowVersion)).OriginalValue =
            rowVersion;
        feedbackDbContext.Feedbacks.Update(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task IFeedbackRepository.CreateFeedbacksAsync(IEnumerable<Domain.Entities.Feedback> feedbacks)
    {
        await feedbackDbContext.AddRangeAsync(feedbacks);
        await feedbackDbContext.SaveChangesAsync();
    }
}