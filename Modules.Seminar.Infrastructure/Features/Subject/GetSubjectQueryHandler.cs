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
using SharedKernel.Dto.Features.Class.Query;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Dto.Features.Subject.Query;

namespace Module.Semester.Infrastructure.Features.Subject
{
    public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, Result<GetSubjectResponse?>>
    {
        private readonly SemesterDbContext _semesterDbContext;
        private readonly IMapper _mapper;

        public GetSubjectQueryHandler(SemesterDbContext semesterDbContext)
        {
            _semesterDbContext = semesterDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Subject, GetSubjectResponse>();
                cfg.CreateMap<Domain.Entities.Lecture, GetLectureResponse>();
            }).CreateMapper();
        }

        async Task<Result<GetSubjectResponse?>> IRequestHandler<GetSubjectQuery, Result<GetSubjectResponse?>>.Handle(
            GetSubjectQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getSubjectResponse = await _semesterDbContext.Subjects
                    .AsNoTracking()
                    .Where(s => s.Id == request.GetSubjectRequest.Id)
                    .ProjectTo<GetSubjectResponse>(_mapper.ConfigurationProvider)
                    .SingleAsync(cancellationToken);

                return Result<GetSubjectResponse?>.Create("Det Specifikke fag fundet", getSubjectResponse,
                    ResultStatus.Success);
            }
            catch (Exception e)
            {
                return Result<GetSubjectResponse?>.Create(e.Message, null, ResultStatus.Error);
            }
        }
    }
}
