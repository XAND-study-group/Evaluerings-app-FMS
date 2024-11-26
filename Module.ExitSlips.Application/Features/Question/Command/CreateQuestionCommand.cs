using MediatR;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Question.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Question.Command;

public record CreateQuestionCommand(CreateQuestionRequest createQuestionRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Result<bool>>
{
    private readonly IExitSlipRepository _exitSlipRepository;

    public CreateQuestionCommandHandler(IExitSlipRepository exitSlipRepository)
    {
        _exitSlipRepository = exitSlipRepository;
    }
    public async Task<Result<bool>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createQuestionRequest = request.createQuestionRequest;
            var exitSlip = await _exitSlipRepository.GetExitSlipByIdAsync(createQuestionRequest.ExitSlipId);
            if (exitSlip is null)
                return Result<bool>.Create("Exitslip blev ikke fundet", false, ResultStatus.Error);

            // Do
            var question = exitSlip.AddQuestion(createQuestionRequest.Text, createQuestionRequest.UserId);

            // Save
            await _exitSlipRepository.CreateQuestionAsync(question);

            return Result<bool>.Create("Spørgsmål blev oprettet", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}
