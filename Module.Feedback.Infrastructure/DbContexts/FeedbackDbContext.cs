using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.Entities;
using Module.Feedback.Domain.WrapperObjects;
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

        modelBuilder.Entity<Room>(r =>
        {
            r.Property(room => room.Id).ValueGeneratedOnAdd();
            r.Property(room => room.RowVersion).IsRowVersion();
            r.ComplexProperty(room => room.Title, room => room.IsRequired());
            r.ComplexProperty(room => room.Description, room => room.IsRequired());
            r.HasMany(room => room.ClassIds).WithMany();
            r.HasMany(room => room.NotificationSubscribedUserIds).WithMany();
        });

        #endregion Room OnModelCreating Configuration

        #region ClassId WO OnModelCreating Configuration

        modelBuilder.Entity<ClassId>(c =>
        {
            c.Property(classId => classId.Id).ValueGeneratedOnAdd();
            c.Property(classId => classId.RowVersion).IsRowVersion();
        });

        #endregion

        #region NotificationUserId WO OnModelCreating Configuration

        modelBuilder.Entity<NotificationUserId>(n =>
        {
            n.Property(notificationUserId => notificationUserId.Id).ValueGeneratedOnAdd();
            n.Property(notificationUserId => notificationUserId.RowVersion).IsRowVersion();
        });

        #endregion

        #region Feedback OnModelCreating Configuration

        modelBuilder.Entity<Domain.Entities.Feedback>(f =>
        {
            f.Property(feedback => feedback.Id).ValueGeneratedOnAdd();
            f.Property(feedback => feedback.RowVersion).IsRowVersion();
            f.ComplexProperty(feedback => feedback.Title, feedback => feedback.IsRequired());
            f.ComplexProperty(feedback => feedback.Problem, feedback => feedback.IsRequired());
            f.ComplexProperty(feedback => feedback.Solution, feedback => feedback.IsRequired());
            f.ComplexProperty(feedback => feedback.HashedUserId, feedback => feedback.IsRequired());
            f.HasMany(feedback => feedback.Comments).WithOne().OnDelete(DeleteBehavior.Cascade);
            f.HasMany(feedback => feedback.Votes).WithOne().OnDelete(DeleteBehavior.Cascade);
        });

        #endregion Feedback OnModelCreating Configuration

        #region Vote OnModelCreating Configuration

        modelBuilder.Entity<Vote>(v =>
        {
            v.Property(vote => vote.Id).ValueGeneratedOnAdd();
            v.Property(vote => vote.RowVersion).IsRowVersion();
            v.ComplexProperty(vote => vote.HashedUserId, vote => vote.IsRequired());
        });

        #endregion Vote OnModelCreating Configuration

        #region Comment OnModelCreating Configuration

        modelBuilder.Entity<Comment>(c =>
        {
            c.Property(comment => comment.Id).ValueGeneratedOnAdd();
            c.Property(comment => comment.RowVersion).IsRowVersion();
            c.ComplexProperty(comment => comment.CommentText, comment => comment.IsRequired());
        });

        #endregion Comment OnModelCreating Configuration
    }
}