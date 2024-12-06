using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.ExitSlip.Command
{
    public record DeleteExitSlipCommand(DeleteExitSlipRequest DeleteExitSlipRequest) : IRequest<Result<bool>>, ITransactionalCommand;

    public class DeleteExitSlipCommandHandler(IExitSlipRepository exitSlipRepository) : 
        IRequestHandler<DeleteExitSlipCommand, Result<bool>>
    {
        async Task<Result<bool>> IRequestHandler<DeleteExitSlipCommand, Result<bool>>.Handle(DeleteExitSlipCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Load
                var deleteExitSlipRequest = request.DeleteExitSlipRequest;
                var exitSlip = await exitSlipRepository.GetExitSlipByIdAsync(deleteExitSlipRequest.Id);

                // Do
                exitSlip.Delete();

                // Save
                await exitSlipRepository.DeleteExitSlipAsync(exitSlip, deleteExitSlipRequest.RowVersion);
                return Result<bool>.Create("ExitSlip fjernet", true, ResultStatus.Success);
            }
            catch (Exception e)
            {
                return Result<bool>.Create(e.Message, false, ResultStatus.Error);
            }
        }
    }
}
