using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.ExitSlip.Command;

public record CreateExitSlipCommand(CreateExitSlipRequest CreateExitSlipRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateExitSlipCommandHandler(IExitSlipRepository exitSlipRepository) :
    IRequestHandler<CreateExitSlipCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateExitSlipCommand, Result<bool>>
        .Handle(CreateExitSlipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load 
            var createExitSlip = request.CreateExitSlipRequest;

            // Do
            var exitSlip = Domain.Entities.ExitSlip.Create(createExitSlip.SubjectId, createExitSlip.LectureId, createExitSlip.Title,
                createExitSlip.MaxQuestionCount, createExitSlip.ActiveStatus);

            // Save 
            await exitSlipRepository.CreateExitSlipAsync(exitSlip);
            return Result<bool>.Create("ExitSlip oprettet", true, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
            throw;
        }


    }
}
