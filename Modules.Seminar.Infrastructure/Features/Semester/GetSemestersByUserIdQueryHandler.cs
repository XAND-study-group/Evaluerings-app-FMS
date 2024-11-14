using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Semester.Query;
using Module.Semester.Infrastructure.DbContexts;
using Module.Shared.Models;
using SharedKernel.Dto.Features.School.Semester.Query;

namespace Module.Semester.Infrastructure.Features.Semester;

public class GetSemestersByUserIdQueryHandler : IRequestHandler<GetSemestersByUserIdQuery, Result<IEnumerable<GetSemestersResponse>>>
{
    private readonly SemesterDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetSemestersByUserIdQueryHandler(SemesterDbContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Semester, GetSemestersResponse>();
        }).CreateMapper();
    }

    async Task<Result<IEnumerable<GetSemestersResponse>>> IRequestHandler<GetSemestersByUserIdQuery, Result<IEnumerable<GetSemestersResponse>>>.Handle(GetSemestersByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var getSemesterResponses = await _dbContext.Semesters
                .AsNoTracking()
                .Where(s => s.SemesterResponsibles.Any(u => u.Id == request.UserId))
                .ProjectTo<GetSemestersResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<IEnumerable<GetSemestersResponse>>.Create("Fandt semestre tilknyttet bruger",
                getSemesterResponses, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetSemestersResponse>>.Create(e.Message,
                [], ResultStatus.Error);
        }
    }
}