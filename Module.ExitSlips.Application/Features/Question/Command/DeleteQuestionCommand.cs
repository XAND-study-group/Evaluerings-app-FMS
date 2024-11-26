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

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Result<bool>>
{
    private readonly IExitSlipRepository _exitSlipRepository;

    public DeleteQuestionCommandHandler(IExitSlipRepository exitSlipRepository)
    {
        _exitSlipRepository = exitSlipRepository;
    }
    public async Task<Result<bool>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var deleteQuestionRequest = request.DeleteQuestionRequest;
            var question = await _exitSlipRepository.GetQuestionByIdAsync(deleteQuestionRequest.QuestionId);

            // Do & Save
            await _exitSlipRepository.DeleteQuestionAsync(question, question.RowVersion);

            return Result<bool>.Create("Question Deleted", true, ResultStatus.Deleted);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}
