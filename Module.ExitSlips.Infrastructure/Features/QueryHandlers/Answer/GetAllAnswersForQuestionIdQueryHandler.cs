using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Answer
{
    public class GetAllAnswersForQuestionIdQueryHandler : IRequestHandler<GetAllAnswersForQuestionIdQuery, Result<IEnumerable<GetAnswerResponse>>>
    {
        private readonly IExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;

        public GetAllAnswersForQuestionIdQueryHandler(IExitSlipDbContext exitSlipDbContext, IMapper mapper)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetAnswerResponse>>> Handle(GetAllAnswersForQuestionIdQuery query, CancellationToken cancellationToken)
        {
            var answers = await _exitSlipDbContext.Questions
                .AsNoTracking()
                .Include(q => q.Answers)
                .Where(q => q.Id == query.QuestionId)
                .Select(a => a.Answers)
                .ProjectTo<GetAnswerResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            if (answers == null)
                return Result<IEnumerable<GetAnswerResponse>>.Create("Ingen svar blev fundet udfra det gældende spørgsmål", null, ResultStatus.Error);

            return Result<IEnumerable<GetAnswerResponse>>.Create("Success", answers, ResultStatus.Success);
        }
    }
}
