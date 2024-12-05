using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Infrastructure.DbContexts;

namespace Module.ExitSlip.Infrastructure.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ExitSlipDbContext _context;

        public AnswerRepository(ExitSlipDbContext context)
        {
            _context = context;
        }

        public async Task<Answer> GetByIdAsync(Guid id)
            => await _context.Answers.FirstOrDefaultAsync(a => a.Id == id) ??
               throw new ArgumentException("Answer not found");


        public async Task<IEnumerable<Answer>> GetAllAsync()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task AddAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Answer answer)
        {
            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var answer = await GetByIdAsync(id);
            if (answer != null)
            {
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }
    }
}