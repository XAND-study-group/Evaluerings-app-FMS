using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Feedback.Query;
using Module.Feedback.Domain;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Feedback;

public class GetFeedbacksByClassIdOrderByCreatedDateTimeQueryHandler : IRequestHandler<GetFeedbacksByClassIdOrderByCreatedDateTimeQuery, Result<IEnumerable<GetAllFeedbacksResponse>?>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetFeedbacksByClassIdOrderByCreatedDateTimeQueryHandler(FeedbackDbContext feedbackDbContext)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Feedback, GetAllFeedbacksResponse>();
            cfg.CreateMap<Domain.Comment, GetCommentResponse>();
            cfg.CreateMap<Domain.Vote, GetVoteResponse>();
        }).CreateMapper();
    }
    async Task<Result<IEnumerable<GetAllFeedbacksResponse>?>>
        IRequestHandler<GetFeedbacksByClassIdOrderByCreatedDateTimeQuery, Result<IEnumerable<GetAllFeedbacksResponse>?>>
        .Handle(GetFeedbacksByClassIdOrderByCreatedDateTimeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var feedbacks = await _feedbackDbContext.Feedbacks
                .Include(f => f.Room)
                .ThenInclude(r => r.ClassIds)
                .Where(f => f.Room.ClassIds
                    .Any(g => g == request.ClassId))
                .ProjectTo<GetAllFeedbacksResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetAllFeedbacksResponse>?>.Create("Alle evalueringer tilhørende klassens ID fundet",
                feedbacks, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetAllFeedbacksResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}