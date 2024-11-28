using MediatR;
using AutoMapper;
using Module.ExitSlip.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Answer.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.ExitSlip.Application.Features.Answer.Command;

public record CreateAnswerCommand(CreateAnswerRequest CreateAnswerRequest) : IRequest<Result<bool>>, ITransactionalCommand;

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
            var createAnswerRequest = request.CreateAnswerRequest;
            var exitSlip = await _exitSlipRepository.GetExitSlipByIdAsync(createAnswerRequest.ExitSlipId);

            // Do
            exitSlip.AddAnswerToQuestion(createAnswerRequest.userId, createAnswerRequest.QuestionId, createAnswerRequest.Text);

            // Save
            await _exitSlipRepository.UpdateExitSlipAsync(exitSlip, exitSlip.RowVersion);


            return Result<bool>.Create("Svar blev oprettet", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}
