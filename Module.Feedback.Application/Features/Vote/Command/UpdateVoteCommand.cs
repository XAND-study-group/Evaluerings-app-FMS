using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Vote.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Vote.Command;

public record UpdateVoteCommand(UpdateVoteRequest UpdateVoteRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class UpdateVoteCommandHandler(IVoteRepository voteRepository) : IRequestHandler<UpdateVoteCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<UpdateVoteCommand, Result<bool>>.Handle(UpdateVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var updateVoteRequest = request.UpdateVoteRequest;
            var feedback = await voteRepository.GetFeedbackByIdAsync(updateVoteRequest.FeedbackId);

            // Do
            var vote = feedback.UpdateVote(updateVoteRequest.VoteId, updateVoteRequest.UserId, updateVoteRequest.VoteScale);

            // Save
            await voteRepository.UpdateVoteAsync(vote, updateVoteRequest.RowVersion);

            return Result<bool>.Create("Vote opdateret", true, ResultStatus.Updated);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}