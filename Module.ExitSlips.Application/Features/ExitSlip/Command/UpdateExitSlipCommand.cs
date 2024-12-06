using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.ExitSlip.Command
{
    public record UpdateExitSlipCommand(UpdateExitSlipRequest UpdateExitSlipRequest) : IRequest<Result<bool>>, ITransactionalCommand;

    public class UpdateExitSlipCommandHandler(IExitSlipRepository exitSlipRepository) : 
        IRequestHandler<UpdateExitSlipCommand, Result<bool>>
    {
        async Task<Result<bool>> IRequestHandler<UpdateExitSlipCommand, Result<bool>>.Handle(UpdateExitSlipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Load
                var updateExitSlipRequest = request.UpdateExitSlipRequest;
                var exitSlip = await exitSlipRepository.GetExitSlipByIdAsync(updateExitSlipRequest.Id);

                // Do 
                exitSlip.Update(updateExitSlipRequest.Title);

                // Save
                await exitSlipRepository.UpdateExitSlipAsync(exitSlip, updateExitSlipRequest.RowVersion);
                return Result<bool>.Create("ExitSlip opdateret", true, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return Result<bool>.Create(e.Message, false, ResultStatus.Error);
            }
        }
    }
}
