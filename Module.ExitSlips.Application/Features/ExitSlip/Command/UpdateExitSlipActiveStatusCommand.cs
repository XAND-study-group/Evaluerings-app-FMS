using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.ExitSlip.Command
{
    public record UpdateExitSlipActiveStatusCommand(UpdateExitSlipActiveStatusRequest UpdateExitSlipActiveStatusRequest) : IRequest<Result<bool>>, ITransactionalCommand;

    public class UpdateExitSlipActiveStatusHandler(IExitSlipRepository exitSlipRepository) : 
        IRequestHandler<UpdateExitSlipActiveStatusCommand, Result<bool>>
    {
        async Task<Result<bool>> IRequestHandler<UpdateExitSlipActiveStatusCommand, Result<bool>>.Handle(UpdateExitSlipActiveStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Load
                var updateExitSlipRequest = request.UpdateExitSlipActiveStatusRequest;
                var exitSlip = await exitSlipRepository.GetExitSlipByIdAsync(updateExitSlipRequest.Id);

                // Save
                exitSlip.UpdateActiveStatus(updateExitSlipRequest.ActiveStatus);

                // Do
                await exitSlipRepository.UpdateExitSlipActiveStatusAsync(exitSlip, updateExitSlipRequest.RowVersion);
                return Result<bool>.Create("ExitSlip ActiveStatus er opdateret.", true, ResultStatus.Success);
            }
            catch (Exception e)
            {

                return Result<bool>.Create(e.Message, false, ResultStatus.Error);
            }

        }
    }
}
