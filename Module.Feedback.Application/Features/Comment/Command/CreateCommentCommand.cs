using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Comment.Command;
using SharedKernel.Interfaces.DomainServices;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Comment.Command;

public record CreateCommentCommand(CreateCommentRequest CreateCommentRequest) : IRequest<Result<bool>>;

public class CreateCommentCommandHandler(ICommentRepository commentRepository, IAiValidationService aiValidationService)
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
                aiValidationService);

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