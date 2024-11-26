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

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Question
{
    public class GetQuestionsByExitSlipIdQueryHandler : IRequestHandler<GetQuestionsByExitSlipIdQuery, Result<IEnumerable<GetSimpleQuestionsResponse>>>
    {
        private readonly IExitSlipRepository _exitSlipRepository;
        private readonly IMapper _mapper;

        public GetQuestionsByExitSlipIdQueryHandler(IExitSlipRepository exitSlipRepository, IMapper mapper)
        {
            _exitSlipRepository = exitSlipRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleQuestionsResponse>>> Handle(GetQuestionsByExitSlipIdQuery query, CancellationToken cancellationToken)
        {
            var exitSlip = await _exitSlipRepository.GetExitSlipByIdAsync(query.ExitSlipId);
            if (exitSlip == null)
            {
                //return Result<IEnumerable<GetSimpleQuestionsResponse>>.Failure("ExitSlip not found");
            }

            var questions = exitSlip.Questions;
            var response = _mapper.Map<IEnumerable<GetSimpleQuestionsResponse>>(questions);
            return Result<IEnumerable<GetSimpleQuestionsResponse>>.Create("response", response, ResultStatus.Error);
        }
    }
}
