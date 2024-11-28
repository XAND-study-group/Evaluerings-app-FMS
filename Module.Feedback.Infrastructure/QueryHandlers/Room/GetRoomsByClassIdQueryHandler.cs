﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Feedback.Application.Features.Room.Query;
using Module.Feedback.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using SharedKernel.Models;

namespace Module.Feedback.Infrastructure.QueryHandlers.Room;

public class GetRoomsByClassIdQueryHandler : IRequestHandler<GetRoomsByClassIdQuery, Result<IEnumerable<GetAllRoomsResponse>?>>
{
    private readonly FeedbackDbContext _feedbackDbContext;
    private readonly IMapper _mapper;

    public GetRoomsByClassIdQueryHandler(FeedbackDbContext feedbackDbContext, IMapper mapper)
    {
        _feedbackDbContext = feedbackDbContext;
        _mapper = mapper;
    }
    async Task<Result<IEnumerable<GetAllRoomsResponse>?>>
        IRequestHandler<GetRoomsByClassIdQuery, Result<IEnumerable<GetAllRoomsResponse>?>>.Handle(
            GetRoomsByClassIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var rooms = await _feedbackDbContext.Rooms
                .AsNoTracking()
                .Where(r => r.ClassIds
                    .Any(c => c == request.ClassId))
                .ProjectTo<IEnumerable<GetAllRoomsResponse>>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);
        
            return Result<IEnumerable<GetAllRoomsResponse>?>.Create("Fandt forums tilknyttet klasse ID", rooms, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetAllRoomsResponse>?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}