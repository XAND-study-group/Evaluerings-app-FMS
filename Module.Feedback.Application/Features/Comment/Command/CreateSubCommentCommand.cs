using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Application.Services;
using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Evaluering.Comment.Command;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Comment.Command;

public record CreateSubCommentCommand(CreateSubCommentRequest CreateSubCommentRequest) : IRequest<Result<bool>>;

public class CreateSubCommentCommandHandler(
    ICommentRepository commentRepository,
    IValidationServiceProxy iValidationServiceProxy,
    ISchoolApiProxy schoolApiProxy,
    IEmailNotificationProxy emailNotificationProxy) : IRequestHandler<CreateSubCommentCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateSubCommentCommand, Result<bool>>.Handle(
        CreateSubCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createSubCommentRequest = request.CreateSubCommentRequest;
            var feedback = await commentRepository.GetFeedbackByIdAsync(createSubCommentRequest.FeedbackId);

            // Do
            var subComment = await feedback.AddSubCommentAsync(createSubCommentRequest.CommentId,
                createSubCommentRequest.UserId,
                createSubCommentRequest.CommentText,
                iValidationServiceProxy);

            if (feedback.ShouldSendNotification())
            {
                var emails = await schoolApiProxy
                    .GetEmailsByUserIdsAsync(feedback.Room.NotificationSubscribedUserIds
                    .Select(n => n.UserIdValue));
                
                await emailNotificationProxy.SendNotificationAsync(emails, "XAND@gmail.com", feedback);
            }

            // Save
            await commentRepository.CreateCommentAsync(subComment);

            return Result<bool>.Create("Sub comment oprettet", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}