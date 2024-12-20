﻿using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.Entities;
using Module.Feedback.Infrastructure.DbContexts;

namespace Module.Feedback.Infrastructure.Repositories;

public class CommentRepository(FeedbackDbContext feedbackDbContext) : ICommentRepository
{
    async Task<Domain.Entities.Feedback> ICommentRepository.GetFeedbackByIdAsync(Guid feedbackId)
    {
        return await feedbackDbContext.Feedbacks
                   .Include(f => f.Comments)
                   .ThenInclude(c => c.SubComments)
                   .AsSplitQuery()
                   .FirstOrDefaultAsync(f => f.Id == feedbackId) ??
               throw new ArgumentException("Evaluering ikke fundet");
    }

    async Task ICommentRepository.CreateCommentAsync(Comment comment)
    {
        await feedbackDbContext.Comments.AddAsync(comment);
        await feedbackDbContext.SaveChangesAsync();
    }

    async Task<Comment> ICommentRepository.GetCommentByIdAsync(Guid commentId)
    {
        return await feedbackDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId) ??
               throw new ArgumentException("Comment not found");
    }
}