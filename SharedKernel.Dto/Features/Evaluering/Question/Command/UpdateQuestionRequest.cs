using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.Question.Command;
    public record UpdateQuestionRequest(
        Guid QuestionId,
        Guid ExitSlipId,
        string Text,
        byte[] RowVersion);
