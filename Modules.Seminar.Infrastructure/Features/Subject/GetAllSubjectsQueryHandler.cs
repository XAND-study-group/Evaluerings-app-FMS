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
using SharedKernel.Dto.Features.Subject.Query;

namespace Module.Semester.Infrastructure.Features.Subject
{
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, Result< IEnumerable<GetAllSubjectsResponse?>>>
    {
        private readonly SemesterDbContext _semesterDbContext;
        private readonly IMapper _mapper;

        public GetAllSubjectsQueryHandler(SemesterDbContext semesterDbContext, IMapper mapper)
        {
            _semesterDbContext = semesterDbContext;
            _mapper = mapper;
        }

        Task<IEnumerable<GetAllSubjectsResponse?>> IRequestHandler<GetAllSubjectsQuery, IEnumerable<GetAllSubjectsResponse?>>.Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = await _semesterDbContext.Subjects
                    .AsNoTracking()
                    .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var response = new GetAllSubjectsResponse(subjects);

                return Result<GetAllSubjectsResponse?>.Create("Alle fag er fundet", response, ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return Result<GetAllSubjectsResponse?>.Create(ex.Message, null, ResultStatus.Error);
            }
        }
    }
}
