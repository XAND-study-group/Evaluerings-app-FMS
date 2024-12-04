using Microsoft.EntityFrameworkCore;
using Module.Feedback.Domain;

namespace Module.Feedback.Application.Abstractions;

public interface IFeedbackDbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Domain.Feedback> Feedbacks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}