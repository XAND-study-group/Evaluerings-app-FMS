using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain;
using Module.Feedback.Infrastructure.DbContexts;

namespace Module.Feedback.Infrastructure.Repositories;

public class FeedbackRepository(FeedbackDbContext feedbackDbContext) : IFeedbackRepository
{
    async Task<Room> IFeedbackRepository.GetRoomByIAsync(Guid roomId)
        => await feedbackDbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId) ??
           throw new ArgumentException("Room not found");

    async Task IFeedbackRepository.CreateFeedbackAsync(Domain.Feedback feedback)
    {
        await feedbackDbContext.Feedbacks.AddAsync(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task<Domain.Feedback> IFeedbackRepository.GetFeedbackByIdAsync(Guid feedbackId)
    => await feedbackDbContext.Feedbacks.FirstOrDefaultAsync(f => f.Id == feedbackId) ??
       throw new ArgumentException("Feedback not found");

    async Task IFeedbackRepository.DeleteFeedbackAsync(Domain.Feedback feedback, byte[] rowVersion)
    {
        feedbackDbContext.Entry(feedback).Property(nameof(Domain.Feedback.RowVersion)).OriginalValue = rowVersion;
        feedbackDbContext.Feedbacks.Remove(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task IFeedbackRepository.UpdateFeedbackAsync(Domain.Feedback feedback, byte[] rowVersion)
    {
        feedbackDbContext.Entry(feedback).Property(nameof(Domain.Feedback.RowVersion)).OriginalValue = rowVersion;
        feedbackDbContext.Feedbacks.Update(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }
}