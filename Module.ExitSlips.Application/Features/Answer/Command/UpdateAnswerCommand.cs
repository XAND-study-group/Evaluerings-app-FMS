using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Answer.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Answer.Command;

public record UpdateAnswerCommand(UpdateAnswerRequest UpdateAnswerRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, Result<bool>>
{
    private readonly IExitSlipRepository _exitSlipRepository;
    public UpdateAnswerCommandHandler(IExitSlipRepository exitSlipRepository)
    {
        _exitSlipRepository = exitSlipRepository;
    }
    public async Task<Result<bool>> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var updateAnswerRequest = request.UpdateAnswerRequest;
            var exitSlip = await _exitSlipRepository.GetExitSlipByIdAsync(updateAnswerRequest.ExitSlipId);

            // Do
            var answer = exitSlip.UpdateAnswerOnQuestion(updateAnswerRequest.QuestionId, updateAnswerRequest.AnswerId, updateAnswerRequest.Text);

            // Save
            await _exitSlipRepository.UpdateAnswerAsync(answer,updateAnswerRequest.RowVersion);

            return Result<bool>.Create("Svaret blev opdateret", true, ResultStatus.Updated);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}