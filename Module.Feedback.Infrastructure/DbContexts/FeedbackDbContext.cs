using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain;

namespace Module.Feedback.Infrastructure.DbContexts;

public class FeedbackDbContext : DbContext, IFeedbackDbContext
{
    public DbSet<Room> Rooms { get; set; }

    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options)
        : base(options)
    {
    }

    //protected override string ConnectionString { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Room OnModelCreating Configuration

        modelBuilder.Entity<Room>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Room>()
            .Property(r => r.RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Room>()
            .OwnsOne(r => r.Title);
        modelBuilder.Entity<Room>()
            .OwnsOne(r => r.Description);

        #endregion Room OnModelCreating Configuration
    }
}