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
        Task<Domain.Entities.ExitSlip> GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id);
        Task CreateAnswerAsync(Answer answer);
        Task UpdateAnswerAsync(Answer answer, byte[] rowVersion);
    }
}
