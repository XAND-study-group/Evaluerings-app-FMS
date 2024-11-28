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

public record UpdateQuestionCommand(UpdateQuestionRequest UpdateQuestionRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;


public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Result<bool>>
{
    private readonly IExitSlipRepository _exitSlipRepository;
    public UpdateQuestionCommandHandler(IExitSlipRepository exitSlipRepository)
    {
        _exitSlipRepository = exitSlipRepository;
    }
    public async Task<Result<bool>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Load
            var updateQuestionRequest = request.UpdateQuestionRequest;
            var exitSlip = await _exitSlipRepository.GetExitSlipByIdAsync(updateQuestionRequest.ExitSlipId);

            //Do
            var question = exitSlip.UpdateQuestion(updateQuestionRequest.QuestionId, updateQuestionRequest.Text);

            //Save
            await _exitSlipRepository.UpdateQuestionAsync(question, updateQuestionRequest.RowVersion);

            return Result<bool>.Create("Spørgsmål blev opdateret", true, ResultStatus.Updated);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}


