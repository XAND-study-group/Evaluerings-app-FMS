using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.QueryHandlers.ExitSlip
{
    public class GetAllExitSlipsForLectureQueryHandler : IRequestHandler<GetAllExitSlipsForLectureQuery, Result<IEnumerable<GetSimpleExitSlipsResponse?>>>
    {
        private readonly ExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;
        public GetAllExitSlipsForLectureQueryHandler(ExitSlipDbContext exitSlipDbContext)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = new MapperConfiguration(cfg =>
            { cfg.CreateMap<Domain.Entities.ExitSlip, GetSimpleExitSlipsResponse>(); }).CreateMapper();
        }

        async Task<Result<IEnumerable<GetSimpleExitSlipsResponse?>>>
            IRequestHandler<GetAllExitSlipsForLectureQuery, Result<IEnumerable<GetSimpleExitSlipsResponse?>>>.Handle(GetAllExitSlipsForLectureQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var response = await _exitSlipDbContext.ExitSlips
                    .AsNoTracking()
                    .Where(e => e.LectureId == request.lectureId)
                    .ProjectTo<GetSimpleExitSlipsResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return Result<IEnumerable<GetSimpleExitSlipsResponse?>>.Create("ExitSlip Fundet", response, ResultStatus.Success);
            }
            catch (Exception e)
            {

                return Result<IEnumerable<GetSimpleExitSlipsResponse?>>.Create(e.Message, [], ResultStatus.Error);
            }
        }
    }
}


