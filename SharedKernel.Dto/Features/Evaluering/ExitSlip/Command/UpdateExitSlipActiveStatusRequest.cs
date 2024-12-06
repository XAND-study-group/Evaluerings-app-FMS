using SharedKernel.Enums.Features.Evaluering.ExitSlip;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Command
{
    public record UpdateExitSlipActiveStatusRequest(
        Guid Id,
        byte[] RowVersion,
        ExitSlipActiveStatus ActiveStatus
        );

}
