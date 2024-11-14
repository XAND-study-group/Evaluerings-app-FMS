using System.Collections;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Lecture.Query;
using Module.Semester.Infrastructure.DbContexts;
using Module.Shared.Models;
using SharedKernel.Dto.Features.Lecture.Query;

namespace Module.Semester.Infrastructure.Features.Lecture;

public class
    GetLecturesByUserIdQueryHandler : IRequestHandler<GetLecturesByUserIdQuery, Result<IEnumerable<GetAllLecturesResponse>>>
{
    private readonly SemesterDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetLecturesByUserIdQueryHandler(SemesterDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Domain.Entities.Lecture, GetAllLecturesResponse>(); })
            .CreateMapper();
    }

    async Task<Result<IEnumerable<GetAllLecturesResponse>>>
        IRequestHandler<GetLecturesByUserIdQuery, Result<IEnumerable<GetAllLecturesResponse>>>.Handle(
            GetLecturesByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getAllLecturesResponse = await _dbContext.Classes
                .AsNoTracking()
                .Include(c => c.Students)
                .Include(c => c.Subjects)
                .ThenInclude(s => s.Lectures)
                .Where(c => c.Students
                    .Any(u => u.Id == request.userId))
                .Select(c => c.Subjects
                    .Select(s => s.Lectures))
                .AsSplitQuery()
                .ProjectTo<GetAllLecturesResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetAllLecturesResponse>>.Create("Lektioner tilknyttet bruger fundet",
                getAllLecturesResponse, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetAllLecturesResponse>>.Create(e.Message, [],
                ResultStatus.Error);
        }
    }
}