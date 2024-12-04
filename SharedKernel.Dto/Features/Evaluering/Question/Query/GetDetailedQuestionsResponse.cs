using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;

namespace SharedKernel.Dto.Features.Evaluering.Question.Query;

public record GetDetailedQuestionsResponse(
    Guid QuestionId,
    Guid ExitSlipId,
    string Text,
    IEnumerable<GetAnswerResponse> Answers)
{
    public GetDetailedQuestionsResponse() : this(default, default, string.Empty, default) { }
}

