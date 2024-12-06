using AutoMapper;
using School.Domain.Entities;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Dto.Features.School.User.Query;

namespace School.Infrastructure.Mapping;

public class MappingProfileSchool : Profile
{
    public MappingProfileSchool()
    {
        // TODO: Those with formember (split up valueobject) are failing. maybe the valueobject should be send but then it needs to be moved.
        CreateMap<Class, GetDetailedClassResponse>();
        CreateMap<Class, GetSimpleClassResponse>();

        CreateMap<Lecture, GetDetailedLectureResponse>(MemberList.None)
            .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion))
            .ForMember(dest => dest.ClassRoom, opt => opt.MapFrom(src => src.ClassRoom))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.Teachers))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.LectureDate, opt => opt.MapFrom(src => src.LectureDate))
            .ForMember(dest => dest.FromTime, opt => opt.MapFrom(src => src.TimePeriod.From))
            .ForMember(dest => dest.ToTime, opt => opt.MapFrom(src => src.TimePeriod.To))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.TimePeriod.Duration));

        CreateMap<Lecture, GetLectureIdResponse>();
        CreateMap<Lecture, GetSimpleLectureResponse>(MemberList.None)
            .ForMember(dest => dest.FromTime, opt => opt.MapFrom(src => src.TimePeriod.From))
            .ForMember(dest => dest.ToTime, opt => opt.MapFrom(src => src.TimePeriod.To));

        CreateMap<Semester, GetDetailedSemesterResponse>(MemberList.None)
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.EducationRange.Start))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EducationRange.End));
        
        CreateMap<Semester, GetSimpleSemesterResponse>(MemberList.None)
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.EducationRange.Start))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EducationRange.End));

        CreateMap<Subject, GetClassSubjectResponse>();
        CreateMap<Subject, GetDetailedSubjectResponse>();
        CreateMap<Subject, GetSimpleSubjectResponse>();
        CreateMap<Class, GetSubjectsByClassResponse>();
        CreateMap<User, GetClassUserResponse>();
        CreateMap<User, GetLectureUserResponse>();
        CreateMap<User, GetSemesterUserResponse>();
        CreateMap<User, GetDetailedUserResponse>();
        CreateMap<User, GetSimpleUserResponse>();
    }
}