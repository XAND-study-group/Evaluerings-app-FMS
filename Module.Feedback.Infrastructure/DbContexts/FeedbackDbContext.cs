using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.Entities;
using SharedKernel.ValueObjects;

namespace Module.Feedback.Infrastructure.DbContexts;

public class FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) : DbContext(options), IFeedbackDbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Domain.Entities.Feedback> Feedbacks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { set; get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("FeedbackModule");

        #region Room OnModelCreating Configuration

        modelBuilder.Entity<Room>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Room>()
            .Property(r => r.RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Room>(r =>
        {
            r.ComplexProperty(room => room.Title, room => room.IsRequired());
            r.ComplexProperty(room => room.Description, room => room.IsRequired());
        });

        #endregion Room OnModelCreating Configuration

        #region Feedback OnModelCreating Configuration

        modelBuilder.Entity<Domain.Entities.Feedback>()
            .Property(r => r.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Domain.Entities.Feedback>()
            .Property(r => r.RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Domain.Entities.Feedback>()
            .ComplexProperty(f => f.Title);
        modelBuilder.Entity<Domain.Entities.Feedback>()
            .ComplexProperty(f => f.Problem);
        modelBuilder.Entity<Domain.Entities.Feedback>()
            .ComplexProperty(f => f.Solution);
        modelBuilder.Entity<Domain.Entities.Feedback>()
            .ComplexProperty(f => f.HashedUserId);
        modelBuilder.Entity<Domain.Entities.Feedback>()
            .HasMany(f => f.Comments)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Domain.Entities.Feedback>()
            .HasMany(f => f.Votes)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        #endregion Feedback OnModelCreating Configuration

        #region Vote OnModelCreating Configuration

        modelBuilder.Entity<Vote>()
            .Property(v => v.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Vote>()
            .Property(v => v.RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Vote>()
            .ComplexProperty(v => v.HashedUserId);

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