using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Infrastructure.DbContexts;

namespace Module.ExitSlip.Infrastructure.Repositories
{
    public class AnswerRepository(ExitSlipDbContext exitSlipDbContext) : IAnswerRepository
    {
        async Task<Domain.Entities.ExitSlip> IAnswerRepository.GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id)
        {
            return await exitSlipDbContext.ExitSlips
                .Include(e => e.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(e => e.Id == id) ??
                throw new ArgumentException("Kunne ikke finde ExitSlip");
        }
        async Task IAnswerRepository.CreateAnswerAsync(Answer answer)
        {
            await exitSlipDbContext.AddAsync(answer);
            await exitSlipDbContext.SaveChangesAsync();
        }


        async Task IAnswerRepository.UpdateAnswerAsync(Answer answer, byte[] rowVersion)
        {
            exitSlipDbContext.Entry(answer).Property(nameof(Answer.RowVersion)).OriginalValue = rowVersion;
            await exitSlipDbContext.SaveChangesAsync();
        }

    }
}