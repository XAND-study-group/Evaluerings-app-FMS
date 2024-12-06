using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.ExitSlip;

public class GetExitSlipWithAnswersForUserQueryHandler(ExitSlipDbContext exitSlipDbContext, IMapper mapper) :
    IRequestHandler<GetExitSlipWithAnswersForUserQuery, Result<GetExitSlipWithAnswersResponse>>
{
    async Task<Result<GetExitSlipWithAnswersResponse>> IRequestHandler<GetExitSlipWithAnswersForUserQuery,
            Result<GetExitSlipWithAnswersResponse>>
        .Handle(GetExitSlipWithAnswersForUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await exitSlipDbContext.ExitSlips
                               .AsNoTracking()
                               .Where(e => e.Id == request.ExitSlipId)
                               .Include(e => e.Questions)
                               .ThenInclude(q => q.Answers.Where(a => a.UserId == request.userId))
                               .ProjectTo<GetExitSlipWithAnswersResponse>(mapper.ConfigurationProvider)
                               .FirstOrDefaultAsync(cancellationToken) ??
                           throw new ArgumentException("Kunne ikke finde ExitSlip for denne bruger");

            return Result<GetExitSlipWithAnswersResponse>.Create("ExitSLip er fundet", response, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetExitSlipWithAnswersResponse>.Create(e.Message, null!, ResultStatus.Error);
        }
    }
}