using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Query
{
    public record GetDetailsExitSlipResponse(
        Guid LectureId,
        Guid SubjectId,
        string Title,
        int MaxQuestioncount,
        ExitSlipActiveStatus ActiveStatus,
        IEnumerable<GetSimpleQuestionsResponse> Questions);

}
