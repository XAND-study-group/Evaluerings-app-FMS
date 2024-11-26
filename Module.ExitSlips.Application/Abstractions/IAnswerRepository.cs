using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Application.Abstractions
{
    public interface IAnswerRepository
    {
        Task AddAsync(Answer answer);
        Task UpdateAsync(Answer answer);
        Task DeleteAsync(Guid id);
    }
}
