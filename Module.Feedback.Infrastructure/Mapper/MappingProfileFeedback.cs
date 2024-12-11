using AutoMapper;
using Module.Feedback.Domain.Entities;
using SharedKernel.Dto.Features.Evaluering.Feedback.Query;
using SharedKernel.Dto.Features.Evaluering.Room.Query;
using GetCommentResponse = SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetCommentResponse;
using GetVoteResponse = SharedKernel.Dto.Features.Evaluering.Feedback.Query.GetVoteResponse;

namespace Module.Feedback.Infrastructure.Mapper;

public class MappingProfileFeedback : Profile
{
    public MappingProfileFeedback()
    {
        CreateMap<Comment, GetCommentResponse>();
        CreateMap<Vote, GetVoteResponse>();
        CreateMap<Domain.Entities.Feedback, GetAllFeedbacksResponse>();
        CreateMap<Domain.Entities.Feedback, GetFeedbackResponse>();
        CreateMap<Room, GetSimpleRoomResponse>();
        CreateMap<Room, GetDetailedRoomResponse>();
    }
}