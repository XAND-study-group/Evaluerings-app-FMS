using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Lecture.Query;
using Module.Semester.Domain.Entities;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Models;

namespace Module.Semester.Infrastructure.Features.Lecture;

public class GetLectureQueryHandler : IRequestHandler<GetLectureQuery, Result<GetDetailedLectureResponse?>>
{
    private readonly SemesterDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetLectureQueryHandler(SemesterDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Lecture, GetDetailedLectureResponse>();
            cfg.CreateMap<User, GetLectureUserResponse>();
        }).CreateMapper();
    }

    async Task<Result<GetDetailedLectureResponse?>> IRequestHandler<GetLectureQuery, Result<GetDetailedLectureResponse?>>.Handle(GetLectureQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var getLectureResponse = await _dbContext.Lectures
                .AsNoTracking()
                .Where(l => l.Id == request.lectureId)
                .ProjectTo<GetDetailedLectureResponse>(_mapper.ConfigurationProvider)
                .SingleAsync(cancellationToken);

            return Result<GetDetailedLectureResponse?>.Create("Specifikke Lektion fundet", getLectureResponse,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetDetailedLectureResponse?>.Create(e.Message, null,
                ResultStatus.Error);
        }
    }
}