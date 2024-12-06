using AutoMapper;
using School.Domain.Entities;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Dto.Features.School.User.Query;

namespace School.Infrastructure.Mapping
{
    public class MappingProfileSchool : Profile
    {
        public MappingProfileSchool()
        {
            CreateMap<Class, GetDetailedClassResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StudentCapacity, opt => opt.MapFrom(src => src.StudentCapacity))
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.Teachers))
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Subjects));
            CreateMap<Class, GetSimpleClassResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StudentCapacity, opt => opt.MapFrom(src => src.StudentCapacity));

            CreateMap<Lecture, GetDetailedLectureResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RowVersion, opt => opt.MapFrom(src => src.RowVersion))
                .ForMember(dest => dest.LectureTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.FromTime, opt => opt.MapFrom(src => src.TimePeriod.From))
                .ForMember(dest => dest.ToTime, opt => opt.MapFrom(src => src.TimePeriod.To))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.LectureDate))
                .ForMember(dest => dest.ClassRoom, opt => opt.MapFrom(src => src.ClassRoom))
                .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.Teachers));

            CreateMap<Lecture, GetLectureIdResponse>();
            CreateMap<Lecture, GetSimpleLectureResponse>()
                .ForMember(dest => dest.LectureTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.TimePeriod.From))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.TimePeriod.To))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.LectureDate));

            CreateMap<Semester, GetDetailedSemesterResponse>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.EducationRange.Start))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EducationRange.End))
                .ForMember(dest => dest.Responsibles, opt => opt.MapFrom(src => src.SemesterResponsibles));
            CreateMap<Semester, GetSimpleSemesterResponse>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.EducationRange.Start))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EducationRange.End));

            CreateMap<Subject, GetClassSubjectResponse>();
            CreateMap<Subject, GetDetailedSubjectResponse>();
            CreateMap<Subject, GetSimpleSubjectResponse>();
            CreateMap<Class, GetSubjectsByClassResponse>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Subjects));

            CreateMap<User, GetClassUserResponse>();
            CreateMap<User, GetLectureUserResponse>();
            CreateMap<User, GetSemesterUserResponse>();
            CreateMap<User, GetDetailedUserResponse>();
            CreateMap<User, GetSimpleUserResponse>();
        }
    }
}
