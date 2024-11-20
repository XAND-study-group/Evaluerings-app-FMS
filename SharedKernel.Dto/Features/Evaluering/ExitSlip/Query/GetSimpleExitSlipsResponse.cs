using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;

public record GetSimpleExitSlipsResponse(
    Guid LectureId,
    string Title,
    int MaxQuestionCount,
    ExitSlipActiveStatus ActiveStatus,
    IEnumerable<GetSimpleQuestionsResponse> Questions);




