using SharedKernel.Dto.Features.Evaluering.ValueObjects;
using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;

public record GetSimpleExitSlipsResponse(
    Guid Id,
    Guid LectureId,
    Guid SubjectId,
    string Title,
    MaxQuestionCountResponse MaxQuestionCount,
    ExitSlipActiveStatus ActiveStatus,
    byte[] RowVersion);