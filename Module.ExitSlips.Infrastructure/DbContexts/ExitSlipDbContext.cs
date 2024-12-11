using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Infrastructure.DbContexts;

public class ExitSlipDbContext(DbContextOptions<ExitSlipDbContext> options) : DbContext(options), IExitSlipDbContext
{
    public DbSet<Domain.Entities.ExitSlip> ExitSlips { get; set; }
    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ExitSlipModule");

        #region Exitslip OnModelCreating Configuration

        modelBuilder.Entity<Domain.Entities.ExitSlip>(e =>
        {
            e.Property(exitSlip => exitSlip.Id).ValueGeneratedOnAdd();
            e.Property(exitSlip => exitSlip.RowVersion).IsRowVersion();
            e.ComplexProperty(exitSlip => exitSlip.Title, exitSlip => exitSlip.IsRequired());
            e.ComplexProperty(exitSlip => exitSlip.MaxQuestionCount, exitSlip => exitSlip.IsRequired());
        });

        #endregion

        #region Question OnModelCreating Configuration

        modelBuilder.Entity<Question>(q =>
        {
            q.Property(question => question.Id).ValueGeneratedOnAdd();
            q.Property(question => question.RowVersion).IsRowVersion();
            q.ComplexProperty(question => question.Text, question => question.IsRequired());
        });

        #endregion

        #region Answer OnModelCreating Configuration

        modelBuilder.Entity<Answer>(a =>
        {
            a.Property(answer => answer.Id).ValueGeneratedOnAdd();
            a.Property(answer => answer.RowVersion).IsRowVersion();
            a.ComplexProperty(answer => answer.Text, answer => answer.IsRequired());
        });

        #endregion
    }
}