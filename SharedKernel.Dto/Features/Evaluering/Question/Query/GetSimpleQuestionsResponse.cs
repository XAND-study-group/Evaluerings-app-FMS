using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;

namespace SharedKernel.Dto.Features.Evaluering.Question.Query;

public record GetSimpleQuestionsResponse(
    Guid QuestionId,
    Guid ExitSlipId,
    string Text,
    IEnumerable<GetAnswerResponse> Answers)
{
    public GetSimpleQuestionsResponse() : this(Guid.Empty, Guid.Empty, string.Empty, Enumerable.Empty<GetAnswerResponse>()) { }
}




//Jeg er I gang med at lave Extesions til de EXitSLips og de to andre. skal finde ud af at mappe dem ordenligt. 