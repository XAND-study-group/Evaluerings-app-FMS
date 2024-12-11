using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Feedback.Query;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Feedback;

public class GetFeedbacksByClassIdOrderByCreatedDateTimeQueryHandler : IRequestHandler<
    GetFeedbacksByClassIdOrderByCreatedDateTimeQuery, Result<IEnumerable<GetSimpleFeedbackResponse>?>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetFeedbacksByClassIdOrderByCreatedDateTimeQueryHandler(FeedbackDbContext feedbackDbContext, IMapper mapper)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = mapper;
    }

    async Task<Result<IEnumerable<GetSimpleFeedbackResponse>?>>
        IRequestHandler<GetFeedbacksByClassIdOrderByCreatedDateTimeQuery, Result<IEnumerable<GetSimpleFeedbackResponse>?>>
        .Handle(GetFeedbacksByClassIdOrderByCreatedDateTimeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackDbContext.Feedbacks
                .AsNoTracking()
                .Include(f => f.Room)
                .ThenInclude(r => r.ClassIds)
                .Where(f => f.Room.ClassIds
                    .Any(g => g.ClassIdValue == request.ClassId))
                .OrderBy(f => f.Created)
                .AsSplitQuery()
                .Skip(request.ItemsPerPage * (request.Page - 1))
                .Take(request.ItemsPerPage)
                .ProjectTo<GetSimpleFeedbackResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetSimpleFeedbackResponse>?>.Create(
                "Alle evalueringer tilhørende klassens ID fundet",
                feedbacks, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetSimpleFeedbackResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}