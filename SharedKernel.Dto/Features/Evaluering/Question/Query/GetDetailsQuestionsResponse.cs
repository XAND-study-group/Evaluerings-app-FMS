using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;

namespace SharedKernel.Dto.Features.Evaluering.Question.Query
{
    public record GetDetailsQuestionsResponse(
            Guid QuestionId,
            Guid ExitSlipId,
            string Text,
            IEnumerable<GetSimpleAnswerResponse> Answers);
}
