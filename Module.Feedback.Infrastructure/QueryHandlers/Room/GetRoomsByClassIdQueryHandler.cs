using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Room.Query;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Room;

public class GetRoomsByClassIdQueryHandler : IRequestHandler<GetRoomsByClassIdQuery, Result<IEnumerable<GetSimpleRoomResponse>?>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetRoomsByClassIdQueryHandler(FeedbackDbContext feedbackDbContext)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Room, GetSimpleRoomResponse>();
        }).CreateMapper();
    }
    async Task<Result<IEnumerable<GetSimpleRoomResponse>?>>
        IRequestHandler<GetRoomsByClassIdQuery, Result<IEnumerable<GetSimpleRoomResponse>?>>.Handle(
            GetRoomsByClassIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var rooms = await _feedbackDbContext.Rooms
                .AsNoTracking()
                .Where(r => r.ClassIds
                    .Any(c => c == request.ClassId))
                .ProjectTo<GetSimpleRoomResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        
            return Result<IEnumerable<GetSimpleRoomResponse>?>.Create("Fandt forums tilknyttet klasse ID", rooms, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetSimpleRoomResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}