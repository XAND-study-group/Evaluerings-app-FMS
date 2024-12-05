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
        Task<Domain.Entities.ExitSlip> GetExitSlipByIdAsync(Guid id);
        Task<Domain.Entities.ExitSlip> GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id);
        Task CreateExitSlipAsync(Domain.Entities.ExitSlip exitSlip);
        Task UpdateExitSlipAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion);
        Task UpdateExitSlipActiveStatusAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion);
        Task DeleteExitSlipAsync(Domain.Entities.ExitSlip exitSlip, byte[] rowVersion);
    }
}
