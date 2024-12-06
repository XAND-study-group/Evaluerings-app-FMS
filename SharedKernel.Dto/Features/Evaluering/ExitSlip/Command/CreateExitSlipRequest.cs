using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;

public record CreateExitSlipRequest(
    Guid SubjectId,
    Guid LectureId,
    string Title,
    int MaxQuestionCount,
    ExitSlipActiveStatus ActiveStatus);