using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain;
using Module.Feedback.Infrastructure.DbContexts;

namespace Module.Feedback.Infrastructure.Repositories;

public class VoteRepository(FeedbackDbContext feedbackDbContext) : IVoteRepository
{
    async Task<Domain.Feedback> IVoteRepository.GetFeedbackByIdAsync(Guid feedbackId)
    => await feedbackDbContext.Feedbacks.SingleAsync(f => f.Id == feedbackId);

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