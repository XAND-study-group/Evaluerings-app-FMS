using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.Entities;
using Module.Feedback.Infrastructure.DbContexts;

namespace Module.Feedback.Infrastructure.Repositories;

public class VoteRepository(FeedbackDbContext feedbackDbContext) : IVoteRepository
{
    async Task<Domain.Entities.Feedback> IVoteRepository.GetFeedbackByIdAsync(Guid feedbackId)
    {
        return await feedbackDbContext.Feedbacks.Include(f => f.Votes)
                   .Include(f => f.Comments)
                   .Include(f => f.Room)
                   .ThenInclude(r => r.NotificationSubscribedUserIds).FirstOrDefaultAsync(f => f.Id == feedbackId) ??
               throw new ArgumentException("Feedback not found");
    }

    async Task IVoteRepository.CreateVoteAsync(Vote vote)
    {
        await feedbackDbContext.Votes.AddAsync(vote);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task IVoteRepository.UpdateVoteAsync(Vote vote, byte[] rowVersion)
    {
        feedbackDbContext.Entry(vote).Property(nameof(Vote.RowVersion)).OriginalValue = rowVersion;
        feedbackDbContext.Votes.Update(vote);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task IVoteRepository.DeleteVoteAsync(Vote vote, byte[] rowVersion)
    {
        feedbackDbContext.Entry(vote).Property(nameof(Vote.RowVersion)).OriginalValue = rowVersion;
        feedbackDbContext.Votes.Remove(vote);
        await feedbackDbContext.SaveChangesAsync();
    }
}