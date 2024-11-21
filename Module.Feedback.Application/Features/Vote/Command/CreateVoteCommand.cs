using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Evaluering.Vote.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Vote.Command;

public record CreateVoteCommand(CreateVoteRequest CreateVoteRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateVoteCommandHandler(IVoteRepository voteRepository, IHashIdService hashIdService)
    : IRequestHandler<CreateVoteCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateVoteCommand, Result<bool>>.Handle(CreateVoteCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createVoteRequest = request.CreateVoteRequest;
            var room = await voteRepository.GetRoomByIdAsync(createVoteRequest.RoomId);

            // Do
            var vote = room.AddVoteToFeedback(createVoteRequest.FeedbackId,
                createVoteRequest.UserId,
                createVoteRequest.VoteScale,
                hashIdService);

            // Save
            await voteRepository.CreateVoteAsync(vote);

            return Result<bool>.Create("Vote oprettet", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}