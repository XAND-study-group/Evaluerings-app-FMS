using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Module.ExitSlip.Application.Abstractions;
using Module.ExitSlip.Application.Features.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.QueryHandlers.Answer
{
    public class GetAllAnswersQueryHandler : IRequestHandler<GetAllAnswersQuery, Result<IEnumerable<GetSimpleAnswerResponse>>>
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;

        public GetAllAnswersQueryHandler(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleAnswerResponse>>> Handle(GetAllAnswersQuery query, CancellationToken cancellationToken)
        {
            var answers = await _answerRepository.GetAllAnswersAsync();
            var response = _mapper.Map<IEnumerable<GetSimpleAnswerResponse>>(answers);
            return Result<IEnumerable<GetSimpleAnswerResponse>>.Success(response);
        }
    }
}
