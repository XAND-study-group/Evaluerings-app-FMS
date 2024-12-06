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
        CreateMap<Domain.Entities.Feedback, GetAllFeedbacksResponse>(MemberList.None)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion))
            .ForMember(dest => dest.HashedId, opt => opt.MapFrom(src => src.HashedUserId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Problem, opt => opt.MapFrom(src => src.Problem))
            .ForMember(dest => dest.Solution, opt => opt.MapFrom(src => src.Solution))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes));

        CreateMap<Domain.Entities.Feedback, GetFeedbackResponse>(MemberList.None)
            .ForMember(dest => dest.FeedbackId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.HashedUserId, opt => opt.MapFrom(src => src.HashedUserId))
            .ForMember(dest => dest.Problem, opt => opt.MapFrom(src => src.Problem))
            .ForMember(dest => dest.Solution, opt => opt.MapFrom(src => src.Solution))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes));

        CreateMap<Comment, GetCommentResponse>(MemberList.None)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.CommentText, opt => opt.MapFrom(src => src.CommentText))
            .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created));

        CreateMap<Vote, GetVoteResponse>(MemberList.None)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion))
            .ForMember(dest => dest.HashedId, opt => opt.MapFrom(src => src.HashedUserId))
            .ForMember(dest => dest.VoteScale, opt => opt.MapFrom(src => src.VoteScale));

        CreateMap<Room, GetSimpleRoomResponse>(MemberList.None)
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<Room, GetDetailedRoomResponse>(MemberList.None)
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}