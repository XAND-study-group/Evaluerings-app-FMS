using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.ExitSlip.Domain.Entities;

namespace Module.ExitSlip.Application.Abstractions
{
    public interface IQuestionRepository
    {
        Task<Domain.Entities.ExitSlip> GetExitSlipWithQuestionsAndAnswersByIdAsync(Guid id);
        Task CreateQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question, byte[] rowVersion);
        Task DeleteQuestionAsync(Question question, byte[] rowVerison);
    }
}
