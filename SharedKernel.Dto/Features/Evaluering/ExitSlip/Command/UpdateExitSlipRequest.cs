using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;

public record UpdateExitSlipRequest(
    Guid Id,
    byte[] RowVersion,
    string Title,
    ExitSlipActiveStatus ActiveStatus);