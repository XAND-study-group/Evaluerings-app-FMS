using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Question.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Question
{
    public class GetQuestionsByExitSlipIdQueryHandler : IRequestHandler<GetQuestionsByExitSlipIdQuery, Result<IEnumerable<GetSimpleQuestionsResponse>>>
    {
        private readonly IExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;

        public GetQuestionsByExitSlipIdQueryHandler(IExitSlipDbContext exitSlipDbContext, IMapper mapper)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleQuestionsResponse>>> Handle(GetQuestionsByExitSlipIdQuery query, CancellationToken cancellationToken)
        {
            var questions = await _exitSlipDbContext.ExitSlips
                .Where(e=>e.Id==query.ExitSlipId)
                .SelectMany(e => e.Questions)
                .ProjectTo<GetSimpleQuestionsResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (questions == null)
                return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("Ingen spørgsmål blev fundet for det givne exitslip", null, ResultStatus.Error);

            return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("Succes", questions, ResultStatus.Success);
        }
    }
}
