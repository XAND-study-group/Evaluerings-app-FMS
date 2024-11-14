using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Subject.Query;
using Module.Semester.Infrastructure.DbContexts;
using Module.Shared.Models;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Dto.Features.School.Subject.Query;

namespace Module.Semester.Infrastructure.Features.Subject
{
    public class GetSubjectsByClassQueryHandler : IRequestHandler<GetSubjectsByClassQuery, Result<IEnumerable<GetAllSubjectsResponse>?>>
    {
        private readonly SemesterDbContext _semesterDbContext;
        private readonly IMapper _mapper;

        public GetSubjectsByClassQueryHandler(SemesterDbContext semesterDbContext, IMapper mapper)
        {
            _semesterDbContext = semesterDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Subject, GetAllSubjectsResponse>();
                cfg.CreateMap<Domain.Entities.Lecture, GetLectureIdResponse>();
            }).CreateMapper();
        }

        public async Task<Result<IEnumerable<GetAllSubjectsResponse>?>> Handle(GetSubjectsByClassQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var classEntity = await _semesterDbContext.Classes
                    .AsNoTracking()
                    .Include(c => c.Subjects)
                    .Where(c => c.Id==request.Request.Id)
                    .Select(c=>c.Subjects)
                    .ProjectTo<GetAllSubjectsResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetAllSubjectsResponse>?>.Create("Fagene er fundet", classEntity, ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<GetAllSubjectsResponse>?>.Create(ex.Message, [], ResultStatus.Error);
            }
        }
    }
}
