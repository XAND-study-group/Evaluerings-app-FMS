using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Application.Services;
using SharedKernel.Dto.Features.Evaluering.Vote.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Vote.Command;

public record CreateVoteCommand(CreateVoteRequest CreateVoteRequest) : IRequest<Result<bool>>, ITransactionalCommand;

public class CreateVoteCommandHandler(
    IVoteRepository voteRepository,
    ISchoolApiProxy schoolApiProxy,
    IEmailNotificationProxy emailNotificationProxy)
    : IRequestHandler<CreateVoteCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateVoteCommand, Result<bool>>.Handle(CreateVoteCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createVoteRequest = request.CreateVoteRequest;
            var feedback = await voteRepository.GetFeedbackByIdAsync(createVoteRequest.FeedbackId);

            // Do
            var vote = feedback.AddVote(
                createVoteRequest.UserId,
                createVoteRequest.VoteScale);

            if (feedback.ShouldSendNotification())
            {
                var emails = await schoolApiProxy
                    .GetEmailsByUserIdsAsync(feedback.Room.NotificationSubscribedUserIds
                        .Select(n => n.UserIdValue));

                await emailNotificationProxy.SendNotificationAsync(emails.SuccessResult, "XAND@gmail.com", feedback);
            }

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