using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Question.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Question.Command;

public record CreateQuestionCommand(CreateQuestionRequest createQuestionRequest) : 
    IRequest<Result<bool>>, ITransactionalCommand;

public class CreateQuestionCommandHandler(IQuestionRepository questionRepository) :
    IRequestHandler<CreateQuestionCommand, Result<bool>>
{
  
    public async Task<Result<bool>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createQuestionRequest = request.createQuestionRequest;
            var exitSlip = await questionRepository.GetExitSlipWithQuestionsAndAnswersByIdAsync(createQuestionRequest.ExitSlipId);

            // Do
            var question = exitSlip.AddQuestion(createQuestionRequest.Text);

            // Save
            await questionRepository.CreateQuestionAsync(question);

            return Result<bool>.Create("Spørgsmål blev oprettet", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}
