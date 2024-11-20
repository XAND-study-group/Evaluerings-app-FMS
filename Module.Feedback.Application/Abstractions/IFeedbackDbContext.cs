using Microsoft.EntityFrameworkCore;
using Module.Feedback.Domain;

namespace Module.Feedback.Application.Abstractions;

public interface IFeedbackDbContext
{
    public DbSet<Room> Rooms { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}