using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Vote.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Vote.Command;

public record DeleteVoteCommand(DeleteVoteRequest DeleteVoteRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class DeleteVoteCommandHandler(IVoteRepository voteRepository) : IRequestHandler<DeleteVoteCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<DeleteVoteCommand, Result<bool>>.Handle(DeleteVoteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var deleteVoteRequest = request.DeleteVoteRequest;
            var vote = await voteRepository.GetVoteByIdAsync(deleteVoteRequest.VoteId);

            // Do & Save
            await voteRepository.DeleteVoteAsync(vote, deleteVoteRequest.RowVersion);

            return Result<bool>.Create("Vote Fjernet", true, ResultStatus.Deleted);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}