using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.ExitSlip;

public class GetExitSlipWithQuestionsQueryHandler(IExitSlipDbContext exitSlipDbContext, IMapper mapper)
    : IRequestHandler<GetExitSlipWithQuestionsQuery,
        Result<IEnumerable<GetDetailedExitSlipResponse>?>>
{
    async Task<Result<IEnumerable<GetDetailedExitSlipResponse>?>> IRequestHandler<GetExitSlipWithQuestionsQuery,
            Result<IEnumerable<GetDetailedExitSlipResponse>?>>
        .Handle(GetExitSlipWithQuestionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await exitSlipDbContext.ExitSlips
                .Where(e => e.Id == request.exitSlipId)
                .Include(e => e.Questions)
                .ProjectTo<GetDetailedExitSlipResponse>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<IEnumerable<GetDetailedExitSlipResponse>?>.Create("ExitSlip med spørgsmål", response,
                ResultStatus.Success);
        }
        catch (Exception e)
        {
            return Result<IEnumerable<GetDetailedExitSlipResponse>?>.Create(e.Message, [], ResultStatus.Error);
        }
    }
}