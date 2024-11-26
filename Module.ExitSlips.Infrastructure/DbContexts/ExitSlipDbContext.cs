using Evaluering.Module.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using SharedKernel.ValueObjects;
using System.Runtime.ConstrainedExecution;


namespace Module.ExitSlip.Infrastructure.DbContexts;

public class ExitSlipDbContext : EvalueringDbContext , IExitSlipDbContext
{
    public DbSet<Domain.Entities.ExitSlip> ExitSlips { get; set; }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public async Task<IEnumerable<Answer>> GetAnswersByQuestionId(Guid questionId)
    {

        var question = Questions.Single(q => q.Id == questionId);

        if(question is null)
            throw new InvalidOperationException("Spørgsmål blev ikke fundet");

        return question.Answers;
    }

    public async Task<IEnumerable<Answer>> GetAnswerByUserId(Guid userId)
    {
        var answers = Answers.Where(a => a.UserId == userId);
        if(answers is null)
            throw new InvalidOperationException("Svar blev ikke fundet");

        return answers;
    }


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
            .ComplexProperty<Title>(e => e.Title);
        modelBuilder.Entity<Domain.Entities.ExitSlip>()
            .ComplexProperty(e => e.MaxQuestionCount);
        modelBuilder.Entity<Domain.Entities.ExitSlip>()
           .ComplexProperty(e => e.ActiveStatus);





        #endregion
    }
}







