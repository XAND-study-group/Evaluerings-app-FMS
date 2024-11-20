using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain;
using Module.Feedback.Infrastructure.DbContexts;

namespace Module.Feedback.Infrastructure.Repositories;

public class CommentRepository(FeedbackDbContext feedbackDbContext) : ICommentRepository
{
    async Task<Domain.Feedback> ICommentRepository.GetFeedbackByIdAsync(Guid feedbackId)
    => await feedbackDbContext.Feedbacks.SingleAsync(f => f.Id == feedbackId);

    async Task ICommentRepository.CreateCommentAsync(Comment comment)
    {
        await feedbackDbContext.Comments.AddAsync(comment);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task<Comment> ICommentRepository.GetCommentByIdAsync(Guid commentId)
    => await feedbackDbContext.Comments.SingleAsync(c => c.Id == commentId);
}