using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain;

namespace Module.Feedback.Infrastructure.DbContexts;

public class FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : DbContext(options), IFeedbackDbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Domain.Feedback> Feedbacks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }

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
            .ComplexProperty(r => r.Title);
        modelBuilder.Entity<Room>()
            .ComplexProperty(r => r.Description);

        #endregion Room OnModelCreating Configuration

        #region Feedback OnModelCreating Configuration

        modelBuilder.Entity<Domain.Feedback>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Domain.Feedback>()
            .Property(r => r.RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Domain.Feedback>()
            .ComplexProperty(f => f.Title);
        modelBuilder.Entity<Domain.Feedback>()
            .ComplexProperty(f => f.Problem);
        modelBuilder.Entity<Domain.Feedback>()
            .ComplexProperty(f => f.Solution);
        modelBuilder.Entity<Domain.Feedback>()
            .ComplexProperty(f => f.HashedUserUserId);
        modelBuilder.Entity<Domain.Feedback>()
            .Property(f => f.Created)
            .ValueGeneratedOnAdd();

        #endregion Feedback OnModelCreating Configuration

        #region Vote OnModelCreating Configuration
        modelBuilder.Entity<Vote>()
            .Property(v => v.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Vote>()
            .Property(v => v.RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Vote>()
            .ComplexProperty(v => v.HashedUserUserId);

        #endregion Vote OnModelCreating Configuration
        
        #region Comment OnModelCreating Configuration
        modelBuilder.Entity<Comment>()
            .Property(v => v.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Comment>()
            .Property(v => v.RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Comment>()
            .ComplexProperty(c => c.CommentText);
        modelBuilder.Entity<Comment>()
            .Property(c => c.Created)
            .ValueGeneratedOnAdd();

        #endregion Comment OnModelCreating Configuration
    }
}