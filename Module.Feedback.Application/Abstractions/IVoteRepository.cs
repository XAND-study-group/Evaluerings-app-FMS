using Module.Feedback.Domain.Entities;

namespace Module.Feedback.Application.Abstractions;

public interface IVoteRepository
{
    Task<Domain.Entities.Feedback> GetFeedbackByIdAsync(Guid feedbackId);
    Task CreateVoteAsync(Vote vote);
    Task UpdateVoteAsync(Vote vote, byte[] rowVersion);
    Task DeleteVoteAsync(Vote vote, byte[] rowVersion);
}