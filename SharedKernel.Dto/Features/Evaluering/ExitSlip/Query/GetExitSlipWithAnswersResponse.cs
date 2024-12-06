using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;

public record GetExitSlipWithAnswersResponse(
    Guid LectureId,
    Guid SubjectId,
    string Title,
    ExitSlipActiveStatus ActiveStatus,
    IEnumerable<GetDetailedQuestionsResponse> Questions,
    byte[] RowVersion);