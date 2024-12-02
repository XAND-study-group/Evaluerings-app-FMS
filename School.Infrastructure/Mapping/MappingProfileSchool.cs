using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            CreateMap<Subject, GetClassSubjectResponse>();
            CreateMap<User, GetClassUserResponse>();
            CreateMap<Class, GetDetailedClassResponse>();

            CreateMap<Class, GetSimpleClassResponse>();

            CreateMap<Lecture, GetDetailedLectureResponse>();
            CreateMap<Lecture, GetLectureIdResponse>();
            CreateMap<User, GetLectureUserResponse>();
            CreateMap<Lecture, GetSimpleLectureResponse>();

            CreateMap<Semester, GetDetailedSemesterResponse>();
            CreateMap<User, GetSemesterUserResponse>();
            CreateMap<Semester, GetSimpleSemesterResponse>();

            CreateMap<Subject, GetDetailedSubjectResponse>();
            CreateMap<Subject, GetSimpleSubjectResponse>();
            CreateMap<Subject, GetSubjectsByClassResponse>();

            CreateMap<User, GetDetailedUserResponse>();
            CreateMap<User, GetSimpleUserResponse>();
        }
    }
}
