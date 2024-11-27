using Module.Feedback.Domain;

namespace Module.Feedback.Application.Abstractions;

public interface IVoteRepository
{
    Task<Domain.Feedback> GetFeedbackByIdAsync(Guid feedbackId);
    Task CreateVoteAsync(Vote vote);
    Task UpdateVoteAsync(Vote vote, byte[] rowVersion);
    Task DeleteVoteAsync(Vote vote, byte[] rowVersion);
}