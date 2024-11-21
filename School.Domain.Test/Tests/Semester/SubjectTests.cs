using School.Domain.Entities;
using School.Domain.Test.Fakes.Semester;
using School.Domain.ValueObjects;
using SharedKernel.ValueObjects;
using Xunit;
using Assert = Xunit.Assert;

namespace School.Domain.Test.Tests.Semester
{
    public class SubjectTests
    {
        #region CreationalTests

        [Theory]
        [MemberData(nameof(ValidCreateData))]
        public void Given_Valid_Data_Then_Subject_Created(SubjectName name, SubjectDescription description, IEnumerable<Subject> otherSubjects)
        {
            // Act
            var subjectSut = FakeSubject.Create(name, description, otherSubjects);

            // Assert
            Assert.NotNull(subjectSut);
        }

        [Theory]
        [InlineData("TestSubject", " ")]
        public void Given_Description_Equal_To_WhiteSpace_Then_Throw_ArgumentException(string name, string description)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => FakeSubject.Create(new SubjectName(name), new SubjectDescription(description), Enumerable.Empty<Subject>()));
        }

        [Fact]
        public void Given_Null_SubjectName_Then_Throw_ArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => FakeSubject.Create(null!, new SubjectDescription("Valid description"), Enumerable.Empty<Subject>()));
        }

        [Fact]
        public void Given_Null_SubjectDescription_Then_Throw_ArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => FakeSubject.Create(new SubjectName("Valid name"), null!, Enumerable.Empty<Subject>()));
        }

        #endregion CreationalTests

        #region UpdateTests

        [Theory]
        [MemberData(nameof(ValidUpdateData))]
        public void Given_Valid_Data_When_Updated_Then_Properties_Updated(SubjectName initialName, SubjectDescription initialDescription, SubjectName newName, SubjectDescription newDescription)
        {
            // Arrange
            var subjectSut = FakeSubject.Create(initialName, initialDescription, Enumerable.Empty<Subject>());

            // Act
            subjectSut.Update(newName, newDescription);

            // Assert
            Assert.Equal(newName, subjectSut.Name);
            Assert.Equal(newDescription, subjectSut.Description);
        }

        [Fact]
        public void Given_Null_SubjectName_When_Updated_Then_Throw_ArgumentNullException()
        {
            // Arrange
            var subjectSut = FakeSubject.Create(new SubjectName("InitialName"), new SubjectDescription("InitialDescription"), Enumerable.Empty<Subject>());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => subjectSut.Update(null!, new SubjectDescription("UpdatedDescription")));
        }

        [Fact]
        public void Given_Null_SubjectDescription_When_Updated_Then_Throw_ArgumentNullException()
        {
            // Arrange
            var subjectSut = FakeSubject.Create(new SubjectName("InitialName"), new SubjectDescription("InitialDescription"), Enumerable.Empty<Subject>());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => subjectSut.Update(new SubjectName("UpdatedName"), null!));
        }

        #endregion UpdateTests

        #region MemberData Methods

        public static IEnumerable<object[]> ValidCreateData()
        {
            yield return new object[]
            {
                    new SubjectName("TestSubject"),
                    new SubjectDescription("This is a valid description."),
                    Enumerable.Empty<Subject>()
            };
        }
        public static IEnumerable<object[]> ValidUpdateData()
        {
            yield return new object[]
            {
                    new SubjectName("InitialName"),
                    new SubjectDescription("InitialDescription"),
                    new SubjectName("UpdatedName"),
                    new SubjectDescription("UpdatedDescription")
            };
        }

        #endregion MemberData Methods
    }
}