using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.Answer.Command;

public record UpdateAnswerRequest(
    Guid AnswerId, 
    Guid QuestionId,
    Guid ExitSlipId,
    string Text,
    byte[] RowVersion);
    

