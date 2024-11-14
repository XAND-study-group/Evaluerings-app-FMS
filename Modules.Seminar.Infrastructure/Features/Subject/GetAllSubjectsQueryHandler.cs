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
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Dto.Features.Subject.Query;

namespace Module.Semester.Infrastructure.Features.Subject
{
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, Result<IEnumerable<GetAllSubjectsResponse>>>
    {
        private readonly SemesterDbContext _semesterDbContext;
        private readonly IMapper _mapper;

        public GetAllSubjectsQueryHandler(SemesterDbContext semesterDbContext, IMapper mapper)
        {
            _semesterDbContext = semesterDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Subject, GetAllSubjectsResponse>();
                cfg.CreateMap<Domain.Entities.Lecture, GetLectureIdResponse>();
            }).CreateMapper();
        }

        public async Task<Result<IEnumerable<GetAllSubjectsResponse>>> Handle(GetAllSubjectsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var subjects = await _semesterDbContext.Subjects
                    .AsNoTracking()
                    .ProjectTo<GetAllSubjectsResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetAllSubjectsResponse>>.Create("Alle fag er fundet", subjects,
                    ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<GetAllSubjectsResponse>>.Create(ex.Message, [], ResultStatus.Error);

            }
        }
    }
}
