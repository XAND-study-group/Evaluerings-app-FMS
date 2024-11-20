using Module.Feedback.Domain;

namespace Module.Feedback.Application.Abstractions;

public interface ICommentRepository
{
    Task<Domain.Feedback> GetFeedbackByIdAsync(Guid feedbackId);
    Task CreateCommentAsync(Comment comment);
    Task<Comment> GetCommentByIdAsync(Guid commentId);
}