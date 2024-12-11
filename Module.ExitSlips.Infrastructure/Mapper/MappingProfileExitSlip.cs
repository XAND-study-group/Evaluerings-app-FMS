using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Module.ExitSlip.Domain.Entities;
using Module.ExitSlip.Domain.ValueObjects;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Dto.Features.Evaluering.ValueObjects;

namespace Module.ExitSlip.Infrastructure.Mapper;

public class MappingProfileExitSlip : Profile
{
    public MappingProfileExitSlip()
    {
        CreateMap<Question, GetSimpleQuestionsResponse>();
        CreateMap<Question, GetDetailedQuestionsResponse>();
        
        CreateMap<Answer, GetDetailedAnswerResponse>();
        
        CreateMap<Domain.Entities.ExitSlip, GetSimpleExitSlipsResponse>();
        CreateMap<Domain.Entities.ExitSlip, GetExitSlipWithAnswersResponse>();
        CreateMap<Domain.Entities.ExitSlip, GetDetailedExitSlipResponse>();
        CreateMap<MaxQuestionCount, MaxQuestionCountResponse>();
    }
}