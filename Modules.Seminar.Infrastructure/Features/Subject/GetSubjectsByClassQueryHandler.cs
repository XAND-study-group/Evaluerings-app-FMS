using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Subject.Query;
using Module.Semester.Infrastructure.DbContexts;
using Module.Shared.Models;
using SharedKernel.Dto.Features.Subject.Query;

namespace Module.Semester.Infrastructure.Features.Subject
{
    public class GetSubjectsByClassQueryHandler : IRequestHandler<GetSubjectsByClassQuery, Result<GetSubjectsByClassResponse?>>
    {
        private readonly SemesterDbContext _semesterDbContext;
        private readonly IMapper _mapper;

        public GetSubjectsByClassQueryHandler(SemesterDbContext semesterDbContext, IMapper mapper)
        {
            _semesterDbContext = semesterDbContext;
            _mapper = mapper;
        }

        public async Task<Result<GetSubjectsByClassResponse?>> Handle(GetSubjectsByClassQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var classEntity = await _semesterDbContext.Classes
                    .AsNoTracking()
                    .Include(c => c.Subjects)
                    .FirstOrDefaultAsync(c => c.Name == request.ClassName, cancellationToken);

                if (classEntity == null)
                {
                    return Result<GetSubjectsByClassResponse?>.Create("Class not found", null, ResultStatus.Error);
                }

                var subjects = classEntity.Subjects
                    .Select(s => _mapper.Map<SubjectDto>(s))
                    .ToList();

                var response = new GetSubjectsByClassResponse(
                    classEntity.Name,
                    subjects
                );

                return Result<GetSubjectsByClassResponse?>.Create("Subjects found", response, ResultStatus.Success);
            }
            catch (Exception ex)
            {
                return Result<GetSubjectsByClassResponse?>.Create(ex.Message, null, ResultStatus.Error);
            }
        }
    }
}
