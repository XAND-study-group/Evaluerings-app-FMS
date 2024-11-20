﻿using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain;

namespace Module.Feedback.Infrastructure.DbContexts;

public class FeedbackDbContext : DbContext, IFeedbackDbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Domain.Feedback> Feedbacks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Vote> Votes { get; set; }

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
            .ComplexProperty(f => f.HashedId);
        modelBuilder.Entity<Domain.Feedback>()
            .Property(f => f.CreatedDateTime)
            .ValueGeneratedOnAdd();

        #endregion Feedback OnModelCreating Configuration
    }
}