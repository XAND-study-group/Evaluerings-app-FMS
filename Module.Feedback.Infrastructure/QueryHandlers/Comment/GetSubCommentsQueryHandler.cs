using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Comment.Query;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Comment;

public class GetSubCommentsQueryHandler : IRequestHandler<GetSubCommentsQuery, Result<IEnumerable<GetCommentResponse>?>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetSubCommentsQueryHandler(FeedbackDbContext feedbackDbContext, IMapper mapper)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = mapper;
    }

    async Task<Result<IEnumerable<GetCommentResponse>?>>
        IRequestHandler<GetSubCommentsQuery, Result<IEnumerable<GetCommentResponse>?>>.Handle(
            GetSubCommentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Get
            var comments = await _feedbackDbContext.Comments
                .AsNoTracking()
                .Where(c => c.Id == request.CommentId)
                .Select(c => c.SubComments)
                .ProjectTo<IEnumerable<GetCommentResponse>>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<IEnumerable<GetCommentResponse>?>.Create("Fandt Sub Comments", comments,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetCommentResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}