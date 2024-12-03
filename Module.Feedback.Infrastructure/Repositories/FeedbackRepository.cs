using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain;
using Module.Feedback.Infrastructure.DbContexts;

namespace Module.Feedback.Infrastructure.Repositories;

public class FeedbackRepository(FeedbackDbContext feedbackDbContext) : IFeedbackRepository
{
    public async Task<Room> GetRoomByIAsync(Guid roomId)
        => await feedbackDbContext.Rooms
            .Include(r => r.ClassIds)
            .SingleAsync(r => r.Id == roomId);

    public async Task CreateFeedbackAsync(Domain.Feedback feedback)
    {
        await feedbackDbContext.Feedbacks.AddAsync(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }

    public async Task<Domain.Feedback> GetFeedbackByIdAsync(Guid feedbackId)
        => await feedbackDbContext.Feedbacks
            .Include(f => f.Room)
            .ThenInclude(r => r.ClassIds)
            .SingleAsync(f => f.Id == feedbackId);

    public async Task DeleteFeedbackAsync(Domain.Feedback feedback, byte[] rowVersion)
    {
        feedbackDbContext.Entry(feedback).Property(nameof(Domain.Feedback.RowVersion)).OriginalValue = rowVersion;
        feedbackDbContext.Feedbacks.Remove(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }

    public async Task UpdateFeedbackAsync(Domain.Feedback feedback, byte[] rowVersion)
    {
        feedbackDbContext.Entry(feedback).Property(nameof(Domain.Feedback.RowVersion)).OriginalValue = rowVersion;
        feedbackDbContext.Feedbacks.Update(feedback);
        await feedbackDbContext.SaveChangesAsync();
    }
}