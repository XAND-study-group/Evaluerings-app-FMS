using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;

public record GetSimpleExitSlipsResponse(
    Guid LectureId,
    Guid SubjectId,
    string Title,
    int MaxQuestionCount,
    ExitSlipActiveStatus ActiveStatus)
{
    public GetSimpleExitSlipsResponse() : this(Guid.Empty, Guid.Empty, string.Empty, 0, ExitSlipActiveStatus.Inactive) { }
}


