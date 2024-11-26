using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<IEnumerable<Answer>> GetAllAsync()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task AddAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, string newText)
        {
            var answer = await GetByIdAsync(id);
            if (answer != null)
            {
                _context.Answers.Update(answer);
                await _context.SaveChangesAsync();
            }
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