using AutoMapper;
using School.Domain.Entities;
using School.Domain.ValueObjects;
using School.Infrastructure.Mapping;
using SharedKernel.Dto.Features.School.Class.Query;
using SharedKernel.Dto.Features.School.Lecture.Query;
using SharedKernel.Dto.Features.School.Semester.Query;
using SharedKernel.Dto.Features.School.Subject.Query;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Enums.Features.Semester;
using System;
using System.Collections.Generic;
using Moq;
using School.Domain.DomainServices.Interfaces;
using SharedKernel.Enums.Features.Authentication;
using Xunit;
using Assert = Xunit.Assert;

namespace School.Domain.Test
{
    public class SchoolAutoMapperConfigurationIsValidTests
    {
        private readonly IMapper _mapper;

        public SchoolAutoMapperConfigurationIsValidTests()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfileSchool>(); });
            _mapper = config.CreateMapper();
        }

        #region Class Mapping Tests
        [Theory]
        [InlineData("Test Class", "Test Description", 30, "Test Subject", "Test Description")]
        public void ShouldMapSubjectToGetClassSubjectResponse(string className, string classDescription,
            int studentCapacity, string subjectName, string subjectDescription)
        {
            // Arrange
            var source = Class.Create(className, classDescription, studentCapacity, new List<Class>());
            var subject = Subject.Create(new SubjectName(subjectName), new SubjectDescription(subjectDescription),
                new List<Subject>());
            source.AddSubject(subject);

            // Act
            var result = _mapper.Map<GetClassSubjectResponse>(subject);

            // Assert
            Assert.Equal(subject.Id, result.Id);
        }

        [Theory]
        [InlineData("Test Class", "Test Description", 30, "Test Semester", 1, 2025, 09, 01, 2026, 01, 15, SchoolType.Fredericia)]
        public void ShouldMapClassToGetDetailedClassResponse(string className, string classDescription, int studentCapacity,
            string semesterName, int semesterNumber,
            int startYear, int startMonth, int startDay,
            int endYear, int endMonth, int endDay, SchoolType schoolType)
        {
            // Arrange
            var semester = Semester.Create(semesterName, semesterNumber, new DateOnly(startYear, startMonth, startDay), new DateOnly(endYear, endMonth, endDay), schoolType, new List<Semester>());
            var source = semester.AddClass(className, classDescription, studentCapacity, new List<Class>());

            // Act
            var result = _mapper.Map<GetDetailedClassResponse>(source);

            // Assert
            Assert.Equal(source.Id, result.Id);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Description, result.Description); 
            Assert.Equal(source.StudentCapacity.Value, result.StudentCapacity);
        }

        [Theory]
        [InlineData("Test Class", "Test Description", 30)]
        public void ShouldMapClassToGetSimpleClassResponse(string className, string classDescription,
            int studentCapacity)
        {
            // Arrange
            var source = Class.Create(className, classDescription, studentCapacity, new List<Class>());

            // Act
            var result = _mapper.Map<GetSimpleClassResponse>(source);

            // Assert
            Assert.Equal(source.Id, result.Id);
            Assert.Equal(source.Name, result.Name);
            Assert.Equal(source.Description, result.Description);
            Assert.Equal(source.StudentCapacity.Value, result.StudentCapacity);
        }
        #endregion

        #region Lecture Mapping Tests
        [Theory]
        [InlineData("Test Class", "Test Description", 30, "Test Subject", "Test Description", "Test Lecture",
            "Test Description", 10, 0, 0, 11, 0, 0, 2024, 12, 1, "Room A")]
        public void ShouldMapLectureToGetDetailedLectureResponse(string className, string classDescription, int studentCapacity,
            string subjectName, string subjectDescription, string lectureTitle, string lectureDescription,
            int lectureStartHour, int lectureStartMinute, int lectureStartSecond, int lectureEndHour,
            int lectureEndMinute, int lectureEndSecond, int lectureYear, int lectureMonth, int lectureDay, string lectureClassRoom)
        {
            // Arrange
            var source = Class.Create(className, classDescription, studentCapacity, new List<Class>());

            var subject = Subject.Create(new SubjectName(subjectName), new SubjectDescription(subjectDescription),
                new List<Subject>());

            source.AddSubject(subject);

            var lecture = subject.AddLecture(lectureTitle, lectureDescription,
                new TimeOnly(lectureStartHour, lectureStartMinute, lectureStartSecond),
                new TimeOnly(lectureEndHour, lectureEndMinute, lectureEndSecond),
                new DateOnly(lectureYear, lectureMonth, lectureDay),
                lectureClassRoom
            );

            // Act
            var result = _mapper.Map<GetDetailedLectureResponse>(lecture);

            // Assert
            Assert.Equal(lecture.Id, result.Id);
            Assert.Equal(lecture.Title.Value, result.LectureTitle);
            Assert.Equal(lecture.Description.Value, result.Description);
            Assert.Equal(lecture.TimePeriod.From, result.FromTime);
            Assert.Equal(lecture.TimePeriod.To, result.ToTime);
            Assert.Equal(lecture.LectureDate.Value, result.Date);
            Assert.Equal(lecture.ClassRoom.Value, result.ClassRoom);
            Assert.Equal(lecture.Teachers.Select(t => t.Id), result.Teachers.Select(t => t.Id));
        }

        [Theory]
        [InlineData("Test Class", "Test Description", 30, "Test Subject", "Test Description", "Test Lecture",
            "Test Description", 10, 0, 0, 11, 0, 0, 2024, 12, 1, "Room A")]
        public void ShouldMapLectureToGetLectureIdResponse(string className, string classDescription,
            int studentCapacity,
            string subjectName, string subjectDescription, string lectureTitle, string lectureDescription,
            int lectureStartHour, int lectureStartMinute, int lectureStartSecond, int lectureEndHour,
            int lectureEndMinute, int lectureEndSecond,
            int lectureYear, int lectureMonth, int lectureDay, string lectureClassRoom)
        {
            // Arrange
            var source = Class.Create(className, classDescription, studentCapacity, new List<Class>());
            var subject = Subject.Create(new SubjectName(subjectName), new SubjectDescription(subjectDescription),
                new List<Subject>());
            source.AddSubject(subject);
            var lecture = subject.AddLecture(lectureTitle, lectureDescription,
                new TimeOnly(lectureStartHour, lectureStartMinute, lectureStartSecond),
                new TimeOnly(lectureEndHour, lectureEndMinute, lectureEndSecond),
                new DateOnly(lectureYear, lectureMonth, lectureDay),
                lectureClassRoom
            );

            // Act
            var result = _mapper.Map<GetLectureIdResponse>(lecture);

            // Assert
            Assert.Equal(lecture.Id, result.Id);
        }

        [Theory]
        [InlineData("test@test.dk", "Test", "User")]
        public void ShouldMapUserToGetLectureUserResponse(string email, string firstName, string lastName)
        {
            // Arrange
            var passwordHasher = new Mock<IPasswordHasher>();
            var user = User.Create(firstName, lastName, email, "Password123!", Role.User, new List<User>(), passwordHasher.Object);

            // Act
            var result = _mapper.Map<GetLectureUserResponse>(user);

            // Assert
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Firstname, result.Firstname);
            Assert.Equal(user.Lastname, result.Lastname);
        }

        [Theory]
        [InlineData("Test Class", "Test Description", 30, "Test Subject", "Test Description", "Test Lecture",
            "Test Description", 10, 0, 0, 11, 0, 0, 2025, 12, 1, "Room A")]
        public void ShouldMapLectureToGetSimpleLectureResponse(string className, string classDescription,
            int studentCapacity,
            string subjectName, string subjectDescription, string lectureTitle, string lectureDescription,
            int lectureStartHour, int lectureStartMinute, int lectureStartSecond, int lectureEndHour,
            int lectureEndMinute, int lectureEndSecond,
            int lectureYear, int lectureMonth, int lectureDay, string lectureClassRoom)
        {
            // Arrange
            var source = Class.Create(className, classDescription, studentCapacity, new List<Class>());
            var subject = Subject.Create(new SubjectName(subjectName), new SubjectDescription(subjectDescription),
                new List<Subject>());
            source.AddSubject(subject);
            var lecture = subject.AddLecture(lectureTitle, lectureDescription,
                new TimeOnly(lectureStartHour, lectureStartMinute, lectureStartSecond),
                new TimeOnly(lectureEndHour, lectureEndMinute, lectureEndSecond),
                new DateOnly(lectureYear, lectureMonth, lectureDay),
                lectureClassRoom
            );

            // Act
            var result = _mapper.Map<GetSimpleLectureResponse>(lecture);

            // Assert
            Assert.Equal(lecture.Title, result.LectureTitle);
            Assert.Equal(lecture.Description, result.Description);
            Assert.Equal(lecture.TimePeriod.From, result.From);
            Assert.Equal(lecture.TimePeriod.To, result.To);
            Assert.Equal(lecture.LectureDate.Value, result.Date);
            Assert.Equal(lecture.ClassRoom, result.ClassRoom);
        }
        #endregion

        #region Semester Mapping Tests
        [Theory]
        [InlineData("Test Semester", 1, 2025, 09, 01, 2026, 01, 15, SchoolType.Fredericia)]
        public void ShouldMapSemesterToGetDetailedSemesterResponse(string semesterName, int semesterNumber,
            int startYear, int startMonth, int startDay,
            int endYear, int endMonth, int endDay, SchoolType schoolType)
        {
            // Arrange
            var otherSemesters = new List<Semester>();
            var semester = Semester.Create(semesterName, semesterNumber, new DateOnly(startYear, startMonth, startDay),
                new DateOnly(endYear, endMonth, endDay), schoolType, otherSemesters);

            // Act
            var result = _mapper.Map<GetDetailedSemesterResponse>(semester);

            // Assert
            Assert.Equal(semester.Id, result.Id);
            Assert.Equal(semester.Name.Value, result.Name);
            Assert.Equal(semester.SemesterNumber.Value, result.SemesterNumber);
            Assert.Equal(semester.EducationRange.Start, result.StartDate);
            Assert.Equal(semester.EducationRange.End, result.EndDate);
            Assert.Equal(semester.School, result.School);
        }
        [Theory]
        [InlineData("Test Semester", 1, 2025, 09, 01, 2026, 01, 15, SchoolType.Fredericia)]
        public void ShouldMapSemesterToGetSimpleSemesterResponse(string semesterName, int semesterNumber, int startYear,
            int startMonth, int startDay,
            int endYear, int endMonth, int endDay, SchoolType schoolType)
        {
            // Arrange
            var otherSemesters = new List<Semester>();
            var semester = Semester.Create(semesterName, semesterNumber, new DateOnly(startYear, startMonth, startDay),
                new DateOnly(endYear, endMonth, endDay), schoolType, otherSemesters);

            // Act
            var result = _mapper.Map<GetSimpleSemesterResponse>(semester);

            // Assert
            Assert.Equal(semester.Id, result.Id);
            Assert.Equal(semester.Name.Value, result.Name);
            Assert.Equal(semester.SemesterNumber.Value, result.SemesterNumber);
            Assert.Equal(semester.EducationRange.Start, result.StartDate);
            Assert.Equal(semester.EducationRange.End, result.EndDate);
            Assert.Equal(semester.School, result.School);
        }
        #endregion

        #region User Mapping Tests
        [Theory]
        [InlineData("test@test.dk", "Test", "User", "Test Semester", 1, 2025, 09, 01, 2026, 01, 15,
            SchoolType.Fredericia)]
        public void ShouldMapUserToGetSemesterUserResponse(string email, string firstName, string lastName,
            string semesterName, int semesterNumber,
            int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay, SchoolType schoolType)
        {
            // Arrange
            var passwordHasher = new Mock<IPasswordHasher>();
            var user = User.Create(firstName, lastName, email, "Password123!", Role.User, new List<User>(),
                passwordHasher.Object);
            var otherSemesters = new List<Semester>();
            var semester = Semester.Create(semesterName, semesterNumber, new DateOnly(startYear, startMonth, startDay),
                new DateOnly(endYear, endMonth, endDay), schoolType, otherSemesters);
            semester.AddResponsible(user);

            // Act
            var result = _mapper.Map<GetSemesterUserResponse>(user);

            // Assert
            Assert.Equal(user.Id, result.Id);
        }

        [Theory]
        [InlineData("test@test.dk", "Test", "User")]
        public void ShouldMapUserToGetClassUserResponse(string email, string firstName, string lastName)
        {
            // Arrange
            var user = User.Create(firstName, lastName, email, "Password123!", Role.User, new List<User>(), null);

            // Act
            var result = _mapper.Map<GetClassUserResponse>(user);

            // Assert
            Assert.Equal(user.Id, result.Id);
        }
        [Theory]
        [InlineData("test@test.dk", "Test", "User", "Test Semester", 1, 2025, 09, 01, 2026, 01, 15,
            SchoolType.Fredericia)]
        public void ShouldMapUserToGetDetailedUserResponse(string email, string firstName, string lastName,
            string semesterName, int semesterNumber,
            int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay, SchoolType schoolType)
        {
            // Arrange
            var passwordHasher = new Mock<IPasswordHasher>();
            var user = User.Create(firstName, lastName, email, "Password123!", Role.User, new List<User>(), passwordHasher.Object);
            var otherSemesters = new List<Semester>();
            var semester = Semester.Create(semesterName, semesterNumber, new DateOnly(startYear, startMonth, startDay), new DateOnly(endYear, endMonth, endDay), schoolType, otherSemesters);
            semester.AddResponsible(user);

            // Act
            var result = _mapper.Map<GetDetailedUserResponse>(user);

            // Assert
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Firstname, result.Firstname);
            Assert.Equal(user.Lastname, result.Lastname);
            Assert.Equal(user.Email.Value, result.Email);
        }

        [Theory]
        [InlineData("test@test.dk", "Test", "User", "Test Semester", 1, 2025, 09, 01, 2026, 01, 15,
            SchoolType.Fredericia)]
        public void ShouldMapUserToGetSimpleUserResponse(string email, string firstName, string lastName,
            string semesterName, int semesterNumber,
            int startYear, int startMonth, int startDay,
            int endYear, int endMonth, int endDay, SchoolType schoolType)
        {
            {
                // Arrange
                var passwordHasher = new Mock<IPasswordHasher>();
                var user = User.Create(firstName, lastName, email, "Password123!", Role.User, new List<User>(), passwordHasher.Object);
                var otherSemesters = new List<Semester>();
                var semester = Semester.Create(semesterName, semesterNumber, new DateOnly(startYear, startMonth, startDay), new DateOnly(endYear, endMonth, endDay), schoolType, otherSemesters);
                semester.AddResponsible(user);

                // Act
                var result = _mapper.Map<GetSimpleUserResponse>(user);

                // Assert
                Assert.Equal(user.Id, result.Id);
                Assert.Equal(user.Firstname, result.Firstname);
                Assert.Equal(user.Lastname, result.Lastname);
                Assert.Equal(user.Email.Value, result.Email);
            }
        }
        #endregion

        #region Subject Mapping Tests
        [Theory]
        [InlineData("Test Class", "Test Description", 30, "Test Subject", "Test Description")]
        public void ShouldMapSubjectToGetDetailedSubjectResponse(string className, string classDescription,
            int studentCapacity, string subjectName, string subjectDescription)
        {
            // Arrange
            var source = Class.Create(className, classDescription, studentCapacity, new List<Class>());
            var subject = Subject.Create(new SubjectName(subjectName), new SubjectDescription(subjectDescription), new List<Subject>());
            source.AddSubject(subject);

            // Act
            var result = _mapper.Map<GetDetailedSubjectResponse>(subject);

            // Assert
            Assert.Equal(subject.Id, result.Id);
            Assert.Equal(subject.Name.Value, result.Name);
            Assert.Equal(subject.Description.Value, result.Description);
        }

        [Theory]
        [InlineData("Test Class", "Test Description", 30, "Test Subject", "Test Description")]
        public void ShouldMapSubjectToGetSimpleSubjectResponse(string className, string classDescription,
            int studentCapacity, string subjectName, string subjectDescription)
        {
            // Arrange
            var source = Class.Create(className, classDescription, studentCapacity, new List<Class>());
            var subject = Subject.Create(new SubjectName(subjectName), new SubjectDescription(subjectDescription),
                new List<Subject>());
            source.AddSubject(subject);

            // Act
            var result = _mapper.Map<GetSimpleSubjectResponse>(subject);

            // Assert
            Assert.Equal(subject.Id, result.Id);
            Assert.Equal(subject.Name.Value, result.Name);
        }
        #endregion

    }
}