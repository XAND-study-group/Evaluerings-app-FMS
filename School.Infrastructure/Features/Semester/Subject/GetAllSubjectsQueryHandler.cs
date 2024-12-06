using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using School.Application.Features.SemesterFeature.Subject.Query;
using School.Infrastructure.DbContext;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Models;

namespace School.Infrastructure.Features.Semester.Subject
{
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, Result<IEnumerable<GetSimpleSubjectResponse>>>
    {
        private readonly SchoolDbContext _semesterDbContext;
        private readonly IMapper _mapper;

        public GetAllSubjectsQueryHandler(SchoolDbContext semesterDbContext, IMapper mapper)
        {
            _semesterDbContext = semesterDbContext;
            _mapper = mapper;
        }

        async Task<Result<IEnumerable<GetSimpleSubjectResponse>>> IRequestHandler<GetAllSubjectsQuery, Result<IEnumerable<GetSimpleSubjectResponse>>>.Handle(
            GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = await _semesterDbContext.Subjects
                    .AsNoTracking()
                    .Include(s => s.Lectures)
                    .ProjectTo<GetSimpleSubjectResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetSimpleSubjectResponse>>.Create("Alle fag er fundet", subjects, ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<GetSimpleSubjectResponse>>.Create(ex.Message, Enumerable.Empty<GetSimpleSubjectResponse>(), ResultStatus.Error);
            }
        }
    }
}
