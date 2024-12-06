using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.ExitSlip;

public class GetExitSlipWithAllAnswersQueryHandler(IExitSlipDbContext exitSlipDbContext, IMapper mapper) :
    IRequestHandler<GetExitSlipWithAllAnswersQuery, Result<GetExitSlipWithAnswersResponse?>>
{
    async Task<Result<GetExitSlipWithAnswersResponse?>>
        IRequestHandler<GetExitSlipWithAllAnswersQuery, Result<GetExitSlipWithAnswersResponse?>>
        .Handle(GetExitSlipWithAllAnswersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await exitSlipDbContext.ExitSlips
                .AsNoTracking()
                .Where(e => e.Id == request.exitSlipId)
                .Include(e => e.Questions)
                .ThenInclude(q => q.Answers)
                .ProjectTo<GetExitSlipWithAnswersResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return Result<GetExitSlipWithAnswersResponse?>.Create("ExitSlip er fundet", response, ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<GetExitSlipWithAnswersResponse?>.Create(e.Message, null, ResultStatus.Error);
        }
    }
}