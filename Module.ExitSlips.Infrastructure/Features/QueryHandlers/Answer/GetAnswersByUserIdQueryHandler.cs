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
    public class GetAnswersByUserIdQueryHandler : IRequestHandler<GetAnswersByUserIdQuery, Result<IEnumerable<GetSimpleAnswerResponse>>>
    {
        private readonly IExitSlipDbContext _exitSlipDbContext;
        private readonly IMapper _mapper;

        public GetAnswersByUserIdQueryHandler(IExitSlipDbContext exitSlipDbContext, IMapper mapper)
        {
            _exitSlipDbContext = exitSlipDbContext;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetSimpleAnswerResponse>>> Handle(GetAnswersByUserIdQuery query, CancellationToken cancellationToken)
        {
            var answers = await _exitSlipDbContext.GetAnswerByUserId(query.UserId);
            if (answers == null)
            {
                return Result<IEnumerable<GetSimpleAnswerResponse>>.Create(
                    "Ingen svar blev fundet udfra det gældende spørgsmål", null, ResultStatus.Error);
            }

            var response = _mapper.Map<IEnumerable<GetSimpleAnswerResponse>>(answers);
            return Result<IEnumerable<GetSimpleAnswerResponse>>.Create("Success", response, ResultStatus.Success);
        } 
    }
}
