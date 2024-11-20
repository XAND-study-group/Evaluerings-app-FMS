using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Semester.Application.Features.Subject.Query;
using Module.Semester.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Lecture.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Dto.Features.Subject.Query;
using SharedKernel.Models;

namespace Module.Semester.Infrastructure.Features.Subject
{
    public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, Result<GetDetailedSubjectResponse?>>
    {
        private readonly SemesterDbContext _semesterDbContext;
        private readonly IMapper _mapper;

        public GetSubjectQueryHandler(SemesterDbContext semesterDbContext)
        {
            _semesterDbContext = semesterDbContext;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Entities.Subject, GetDetailedSubjectResponse>();
                cfg.CreateMap<Domain.Entities.Lecture, GetSimpleLectureResponse>();
            }).CreateMapper();
        }

        async Task<Result<GetDetailedSubjectResponse?>> IRequestHandler<GetSubjectQuery, Result<GetDetailedSubjectResponse?>>.Handle(
            GetSubjectQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getSubjectResponse = await _semesterDbContext.Subjects
                    .AsNoTracking()
                    .Where(s => s.Id == request.GetSubjectRequest.Id)
                    .ProjectTo<GetDetailedSubjectResponse>(_mapper.ConfigurationProvider)
                    .SingleAsync(cancellationToken);

                return Result<GetDetailedSubjectResponse?>.Create("Det Specifikke fag fundet", getSubjectResponse,
                    ResultStatus.Success);
            }
            catch (Exception e)
            {
                return Result<GetDetailedSubjectResponse?>.Create(e.Message, null, ResultStatus.Error);
            }
        }
    }
}
