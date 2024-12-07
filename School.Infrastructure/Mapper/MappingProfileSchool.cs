using AutoMapper;
using School.Domain.Entities;
using School.Domain.ValueObjects;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Dto.Features.School.ValueObjects;

namespace School.Infrastructure.Mapper;

public class MappingProfileSchool : Profile
{
    public MappingProfileSchool()
    {

        #region Class Mappings

        CreateMap<Class, GetDetailedClassResponse>();
        CreateMap<Class, GetSimpleClassResponse>();

        #endregion

        CreateMap<Lecture, GetDetailedLectureResponse>();
        CreateMap<Lecture, GetLectureIdResponse>();
        CreateMap<Lecture, GetSimpleLectureResponse>();
        CreateMap<TimePeriod, TimePeriodResponse>();

        CreateMap<Semester, GetDetailedSemesterResponse>();
        CreateMap<Semester, GetSimpleSemesterResponse>();
        CreateMap<EducationRange, EducationRangeResponse>();

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