namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Command
{
    public record DeleteExitSlipRequest(
        Guid Id,
        byte[] RowVersion);    
}
