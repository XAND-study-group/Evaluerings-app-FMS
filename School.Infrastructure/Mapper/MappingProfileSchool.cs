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
        CreateMap<Class, GetDetailedClassResponse>();
        CreateMap<Class, GetSimpleClassResponse>();

        CreateMap<Lecture, GetDetailedLectureResponse>()
            .ForMember(dest => dest.FromTime, opt => opt.MapFrom(src => src.TimePeriod.From))
            .ForMember(dest => dest.ToTime, opt => opt.MapFrom(src => src.TimePeriod.To));

        CreateMap<Lecture, GetLectureIdResponse>();
        CreateMap<Lecture, GetSimpleLectureResponse>()
            .ForMember(dest => dest.FromTime, opt => opt.MapFrom(src => src.TimePeriod.From))
            .ForMember(dest => dest.ToTime, opt => opt.MapFrom(src => src.TimePeriod.To));

        CreateMap<Semester, GetDetailedSemesterResponse>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.EducationRange.Start))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EducationRange.End));
        
        CreateMap<Semester, GetSimpleSemesterResponse>()
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