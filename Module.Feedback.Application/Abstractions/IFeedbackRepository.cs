using Module.Feedback.Domain.Entities;

namespace Module.Feedback.Application.Abstractions;

public interface IFeedbackRepository
{
    Task<Room> GetRoomByIAsync(Guid roomId);
    Task CreateFeedbackAsync(Domain.Entities.Feedback feedback);
    Task CreateFeedbacksAsync(IEnumerable<Domain.Entities.Feedback> feedbacks);
    Task<Domain.Entities.Feedback> GetFeedbackByIdAsync(Guid feedbackId);
    Task DeleteFeedbackAsync(Domain.Entities.Feedback feedback, byte[] rowVersion);
    Task UpdateFeedbackAsync(Domain.Entities.Feedback feedback, byte[] rowVersion);
}