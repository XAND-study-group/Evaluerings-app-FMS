using Module.Feedback.Domain.Entities;

namespace Module.Feedback.Application.Abstractions;

public interface ICommentRepository
{
    Task<Domain.Entities.Feedback> GetFeedbackByIdAsync(Guid feedbackId);
    Task CreateCommentAsync(Comment comment);
    Task<Comment> GetCommentByIdAsync(Guid commentId);
}