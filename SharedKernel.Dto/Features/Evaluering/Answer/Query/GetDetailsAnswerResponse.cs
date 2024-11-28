using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.Answer.Query
{
    public record GetDetailsAnswerResponse(
            Guid AnswerId,
            string Text,
            Guid QuestionId);
}
