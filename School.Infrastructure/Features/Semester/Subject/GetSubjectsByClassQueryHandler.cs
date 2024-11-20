using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Subject.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Subject
{
    public class GetSubjectsByClassQueryHandler : IRequestHandler<GetSubjectsByClassQuery, Result<IEnumerable<GetDetailedSubjectResponse>?>>
    {
        private readonly SchoolDbContext _semesterDbContext;
        private readonly IMapper _mapper;

        public GetSubjectsByClassQueryHandler(SchoolDbContext semesterDbContext)
        {
            _semesterDbContext = semesterDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Subject, GetDetailedSubjectResponse>();
                cfg.CreateMap<Domain.Entities.Lecture, GetDetailedLectureResponse>();
            }).CreateMapper();
        }

        public async Task<Result<IEnumerable<GetDetailedSubjectResponse>?>> Handle(GetSubjectsByClassQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var classEntity = await _semesterDbContext.Classes
                    .AsNoTracking()
                    .Include(c => c.Subjects)
                    .Where(c => c.Id==request.Request.Id)
                    .Select(c=>c.Subjects)
                    .ProjectTo<GetDetailedSubjectResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetDetailedSubjectResponse>?>.Create("Fagene er fundet", classEntity, ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<GetDetailedSubjectResponse>?>.Create(ex.Message, [], ResultStatus.Error);
            }
        }
    }
}
