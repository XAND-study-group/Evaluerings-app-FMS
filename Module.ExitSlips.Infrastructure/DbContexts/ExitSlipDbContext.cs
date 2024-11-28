﻿using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using SharedKernel.ValueObjects;

namespace Module.ExitSlip.Infrastructure.DbContexts;

public class ExitSlipDbContext : EvalueringDbContext, IExitSlipDbContext
{
    public DbSet<Domain.Entities.ExitSlip> ExitSlips { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public ExitSlipDbContext(DbContextOptions<ExitSlipDbContext> options)
        : base(options)
    {
    }

    protected override string ConnectionString { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region User OnModelCreating Configuration
        modelBuilder.Entity<Domain.Entities.ExitSlip>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Domain.Entities.ExitSlip>()
            .Property(e => e.RowVersion)
            .IsRowVersion();
        modelBuilder.Entity<Domain.Entities.ExitSlip>()
          .Property(e => e.SubjectId);
        modelBuilder.Entity<Domain.Entities.ExitSlip>()
        .Property(e => e.LectureId);
        modelBuilder.Entity<Domain.Entities.ExitSlip>()
            .ComplexProperty(e => e.Title);
        modelBuilder.Entity<Domain.Entities.ExitSlip>()
            .ComplexProperty(e => e.MaxQuestionCount);
        modelBuilder.Entity<Domain.Entities.ExitSlip>()
           .ComplexProperty(e => e.ActiveStatus);

        #endregion
    }
}







