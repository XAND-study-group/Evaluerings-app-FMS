using Module.Semester.Domain.Test.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Module.Semester.Domain.Test
{
    public class SubjectTests
    {
        #region Tests

        [Theory]
        [MemberData(nameof(ValidCreateData))]
        public void Given_Valid_Data_Then_Subject_Created(string name, string description)
        {
            // Act
            var subjectSut = FakeSubject.Create(name, description);

            // Assert
            Assert.NotNull(subjectSut);
        }

        [Theory]
        [MemberData(nameof(WhiteSpaceDescriptionData))]
        public void Given_Description_Equal_To_WhiteSpace_Then_Throw_ArgumentNullException(string name, string description)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => FakeSubject.Create(name, description));
        }

        [Theory]
        [MemberData(nameof(LongDescriptionData))]
        public void Given_Description_Length_Over_500_Then_Throw_ArgumentException(string name, string description)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => FakeSubject.Create(name, description));
        }

        [Theory]
        [MemberData(nameof(ValidUpdateData))]
        public void Given_Valid_Data_When_Updated_Then_Properties_Updated(string initialName, string initialDescription, string newName, string newDescription)
        {
            // Arrange
            var subjectSut = FakeSubject.Create("InitialName", "InitialDescription");

            // Act
            subjectSut.Update(newName, newDescription);

            // Assert
            Assert.Equal(newName, subjectSut.Name);
            Assert.Equal(newDescription, subjectSut.Description);
        }

        #endregion

        #region MemberData Methods

        public static IEnumerable<object[]> ValidCreateData()
        {
            yield return new object[]
            {
                "TestSubject",
                "This is a valid description."
            };
        }
        public static IEnumerable<object[]> WhiteSpaceDescriptionData()
        {
            yield return new object[]
            {
                "TestSubject",
                " "
            };
        }
        public static IEnumerable<object[]> LongDescriptionData()
        {
            yield return new object[]
            {
                "TestSubject",
                string.Concat(Enumerable.Repeat("FakeTestNow", 50))
            };
        }
        public static IEnumerable<object[]> ValidUpdateData()
        {
            yield return new object[]
            {
                "InitialName",
                "InitialDescription",
                "UpdatedName",
                "UpdatedDescription"
            };
        }

        #endregion
    }
}
