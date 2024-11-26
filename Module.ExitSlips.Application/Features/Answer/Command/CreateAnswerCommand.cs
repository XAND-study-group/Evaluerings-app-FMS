using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Answer.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Answer.Command;

public record CreateAnswerCommand(CreateAnswerRequest createAnswerRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, Result<bool>>
{
    private readonly IExitSlipRepository _exitSlipRepository;
    public CreateAnswerCommandHandler(IExitSlipRepository exitSlipRepository)
    {
        _exitSlipRepository = exitSlipRepository;
    }
    public async Task<Result<bool>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createAnswerRequest = request.createAnswerRequest;
            var exitSlip = await _exitSlipRepository.GetExitSlipByIdAsync(createAnswerRequest.ExitslipId);

            // Do
            var answer = exitSlip.AddAnswer(createAnswerRequest.userId, createAnswerRequest.QuestionId, createAnswerRequest.ExitslipId, createAnswerRequest.Text);

            // Save
            await _exitSlipRepository.CreateAnswerAsync(answer);

            return Result<bool>.Create("Answer created", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}
