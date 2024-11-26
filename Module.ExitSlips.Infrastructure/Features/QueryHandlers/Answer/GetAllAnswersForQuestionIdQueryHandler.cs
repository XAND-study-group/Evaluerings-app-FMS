using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Answer.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Answer
{
    public class GetAllAnswersForQuestionIdQueryHandler : IRequestHandler<GetAllAnswersForQuestionIdQuery, Result<IEnumerable<GetSimpleAnswerResponse>>>
    {
        private readonly ExitSlipDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllAnswersForQuestionIdQueryHandler(ExitSlipDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleAnswerResponse>>> Handle(GetAllAnswersForQuestionIdQuery query, CancellationToken cancellationToken)
        {
            var answers = (await _dbContext.Questions
                .Include(q => q.Answers)
                .SingleAsync(q => q.Id == query.QuestionId)).Answers;
            if (answers == null)
            {
                return Result<IEnumerable<GetSimpleAnswerResponse>>.Create("No answers found for the given question ID", [], ResultStatus.Error);
            }

            var response = _mapper.Map<IEnumerable<GetSimpleAnswerResponse>>(answers);
            return Result<IEnumerable<GetSimpleAnswerResponse>>.Create("", response, ResultStatus.Success);
        }
    }
}
