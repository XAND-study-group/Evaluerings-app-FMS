using Module.Feedback.Domain;

namespace Module.Feedback.Application.Abstractions;

public interface IFeedbackRepository
{
    Task<Room> GetRoomByIAsync(Guid roomId);
    Task CreateFeedbackAsync(Domain.Feedback feedback);
    Task<Domain.Feedback> GetFeedbackByIdAsync(Guid feedbackId);
    Task DeleteFeedbackAsync(Domain.Feedback feedback, byte[] rowVersion);
}