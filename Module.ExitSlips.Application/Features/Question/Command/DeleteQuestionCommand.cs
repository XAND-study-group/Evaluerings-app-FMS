using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Question.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Question.Command;

public record DeleteQuestionCommand(DeleteQuestionRequest DeleteQuestionRequest)
        : IRequest<Result<bool>>, ITransactionalCommand;

public class DeleteQuestionCommandHandler(IQuestionRepository questionRepository) :
    IRequestHandler<DeleteQuestionCommand, Result<bool>>
{

    public async Task<Result<bool>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var deleteQuestionRequest = request.DeleteQuestionRequest;
            var exitSlip = await questionRepository.GetExitSlipWithQuestionsAndAnswersByIdAsync(deleteQuestionRequest.ExitSlipId);

            // Do
            var question = exitSlip.DeleteQuestion(deleteQuestionRequest.QuestionId);

            // Save
            await questionRepository.DeleteQuestionAsync(question, deleteQuestionRequest.RowVersion);
            return Result<bool>.Create("Spørgsmål blev slettet", true, ResultStatus.Deleted);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}
