using AutoMapper;
using Module.ExitSlip.Domain.Entities;
using SharedKernel.Dto.Features.Evaluering.Answer.Command;
using SharedKernel.Dto.Features.Evaluering.Answer.Query;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Command;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Dto.Features.Evaluering.Question.Command;
using SharedKernel.Dto.Features.Evaluering.Question.Query;

namespace Module.ExitSlip.Infrastructure.Mapper
{
    public class MappingProfileExitSlip : Profile
    {
        public MappingProfileExitSlip()
        {
            CreateMap<Answer, GetAnswerResponse>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text.Value))
                .ForMember(dest=> dest.AnswerId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Question, GetSimpleQuestionsResponse>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text.Value))
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ExitSlipId, opt => opt.MapFrom(src => src.Id));
            CreateMap<Question, GetDetailedQuestionsResponse>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text.Value))
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ExitSlipId, opt => opt.MapFrom(src => src.Id));

            CreateMap<Domain.Entities.ExitSlip, GetSimpleExitSlipsResponse>();
            CreateMap<Domain.Entities.ExitSlip, GetExitSlipsWithAnswersResponse>();
            CreateMap<Domain.Entities.ExitSlip, GetDetailedExitSlipResponse>();
        }
    }
}