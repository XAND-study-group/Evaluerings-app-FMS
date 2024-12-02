using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.Answer.Query;

public record GetDetailedAnswerResponse(
    Guid AnswerId,
    string Text,
    Guid QuestionId)
{
    public GetDetailedAnswerResponse() : this(default, string.Empty, default) { }
}

