using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.Question.Query
{
    public record GetDetailsQuestionsResponse(
        string Text,
        IEnumerable<GetSimpleAnswerResponse> Answers
        );
}
