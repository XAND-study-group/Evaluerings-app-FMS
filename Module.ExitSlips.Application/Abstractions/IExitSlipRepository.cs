using Module.ExitSlip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Application.Abstractions
{
    public interface IExitSlipRepository
    {
        Task CreateExitSlipAsync(Domain.Entities.ExitSlip exitSlip);

        Task<Domain.Entities.ExitSlip> GetExitSlipByIdAsync(Guid exitSlipId);

        Task UpdateAnswerAsync(Answer answer, byte[] rowVersion);
        Task CreateAnswerAsync(Answer answer);
        Task UpdateQuestionAsync(Question question, byte[] rowVersion);
    }
}
