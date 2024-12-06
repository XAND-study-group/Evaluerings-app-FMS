using MediatR;
using Module.Feedback.Application.Abstractions;
using Module.Feedback.Application.Services;
using Module.Feedback.Domain.DomainServices.Interfaces;
using SharedKernel.Dto.Features.Evaluering.Comment.Command;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Comment.Command;

public record CreateCommentCommand(CreateCommentRequest CreateCommentRequest) : IRequest<Result<bool>>, IApplcationClass;

public class CreateCommentCommandHandler(ICommentRepository commentRepository, IValidationServiceProxy iValidationServiceProxy, 
    IEmailNotificationProxy emailNotificationProxy, ISchoolApiProxy schoolAPIProxy)
    : IRequestHandler<CreateCommentCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createCommentRequest = request.CreateCommentRequest;
            var feedback = await commentRepository.GetFeedbackByIdAsync(createCommentRequest.FeedbackId);

            // Do
            var comment = await feedback.AddComment(
                createCommentRequest.UserId,
                createCommentRequest.CommentText,
                iValidationServiceProxy);

            if (feedback.ShouldSendNotification())
            {
                var emails = await schoolAPIProxy.GetEmailsByUserIdsAsync(feedback.Room.NotificationSubscribedUserIds);
                await emailNotificationProxy.SendNotificationAsync(emails, "XAND@gmail.com", feedback);
            }

            // Save
            await commentRepository.CreateCommentAsync(comment);

            return Result<bool>.Create("Comment oprettet", true, ResultStatus.Created);
        }
        catch (Exception e)
        {
            return Result<bool>.Create(e.Message, false, ResultStatus.Error);
        }
    }
}