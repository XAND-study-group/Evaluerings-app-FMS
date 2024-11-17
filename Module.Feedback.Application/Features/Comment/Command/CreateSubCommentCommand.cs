using MediatR;
using Module.Feedback.Application.Abstractions;
using SharedKernel.Dto.Features.Evaluering.Comment.Command;
using SharedKernel.Interfaces.DomainServices;
using SharedKernel.Models;

namespace Module.Feedback.Application.Features.Comment.Command;

public record CreateSubCommentCommand(CreateSubCommentRequest CreateSubCommentRequest) : IRequest<Result<bool>>;

public class CreateSubCommentCommandHandler(
    ICommentRepository commentRepository,
    IAiValidationService aiValidationService) : IRequestHandler<CreateSubCommentCommand, Result<bool>>
{
    async Task<Result<bool>> IRequestHandler<CreateSubCommentCommand, Result<bool>>.Handle(
        CreateSubCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Load
            var createSubCommentRequest = request.CreateSubCommentRequest;
            var comment = await commentRepository.GetCommentByIdAsync(createSubCommentRequest.CommentId);

            // Do
            var subComment = await Domain.Comment.CreateAsync(createSubCommentRequest.UserId,
                createSubCommentRequest.CommentText,
                aiValidationService);
            comment.AddSubComment(subComment);

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