using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Room.Query;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Room;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<GetDetailedRoomResponse?>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetRoomByIdQueryHandler(FeedbackDbContext feedbackDbContext, IMapper mapper)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = mapper;
    }

    async Task<Result<GetDetailedRoomResponse?>> IRequestHandler<GetRoomByIdQuery, Result<GetDetailedRoomResponse?>>.
        Handle(
            GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _feedbackDbContext.Rooms
                .AsNoTracking()
                .Where(r => r.Id == request.RoomId)
                .ProjectTo<GetDetailedRoomResponse>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<GetDetailedRoomResponse?>.Create("Forum Fundet", response, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetDetailedRoomResponse?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}