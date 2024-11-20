using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Feedback.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Feedback.Command;

public record DeleteFeedbackCommand(DeleteFeedbackRequest DeleteFeedbackRequest)
    : IRequest<Result<bool>>, ITransactionalCommand;

public class DeleteFeedbackCommandHandler(IFeedbackRepository feedbackRepository)
    : IRequestHandler<DeleteFeedbackCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<DeleteFeedbackCommand, Result<bool>>.Handle(DeleteFeedbackCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var deleteFeedbackRequest = request.DeleteFeedbackRequest;
            var feedback = await feedbackRepository.GetFeedbackByIdAsync(deleteFeedbackRequest.FeedbackId);

            // Do & Save
            await feedbackRepository.DeleteFeedbackAsync(feedback, deleteFeedbackRequest.RowVersion);

            return Result<bool>.Create("Evaluering slettet", true, ResultStatus.Deleted);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}