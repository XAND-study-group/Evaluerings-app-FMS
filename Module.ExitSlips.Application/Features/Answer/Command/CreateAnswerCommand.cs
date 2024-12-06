using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Answer.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Answer.Command;

public record CreateAnswerCommand(CreateAnswerRequest CreateAnswerRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateAnswerCommandHandler(IAnswerRepository answerRepository) :
    IRequestHandler<CreateAnswerCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createAnswerRequest = request.CreateAnswerRequest;
            var exitSlip =
                await answerRepository.GetExitSlipWithQuestionsAndAnswersByIdAsync(createAnswerRequest.ExitSlipId);

            // Do
            var answer = exitSlip.AddAnswer(createAnswerRequest.userId, createAnswerRequest.QuestionId,
                createAnswerRequest.Text);

            // Save
            await answerRepository.CreateAnswerAsync(answer);

            return Result<bool>.Create("Svar blev oprettet", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}