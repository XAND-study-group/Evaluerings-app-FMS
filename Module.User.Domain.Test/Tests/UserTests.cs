using Module.User.Domain.Test.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Module.User.Domain.Test.Tests
{
    public class UserTests
    {
        #region Tests

        #region Create Tests

        [Theory]
        [MemberData(nameof(CreateWithValidInput))]
        public void Given_valid_input_then_create_success(string firstname, string lastname, string email)
        {
            // Act
            var user = Entities.User.Create(firstname, lastname, email);

            // Assert
            Assert.NotNull(user);
        }

        [Theory]
        [MemberData(nameof(CreateWithEmptyFirstname))]
        public void Given_empty_firstname_then_throw_argumentException(string firstname, string lastname, string email)
        {
            // Arrange
            var sut = new FakeUser(firstname, lastname, email);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.ValidateName(firstname));
        }


        [Theory]
        [MemberData(nameof(CreateFirstnameWithWhiteSpace))]
        public void Given_firstname_with_white_space_the_throw_argumentException(string firstname, string lastname, string email)
        {
            // Arrange
            var sut = new FakeUser(firstname, lastname, email);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.ValidateName(firstname));

        }

        [Theory]
        [MemberData(nameof(CreateWithInvalidEmail))]
        public void Given_with_Invalid_email_then_throw_argumentException(string firstname, string lastname, string email)
        {
            // Arrange
            var sut = new FakeUser(firstname, lastname, email);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.ValidateEmail(email));

        }

        #endregion


        #endregion


        #region MemberData Methodes


        public static IEnumerable<object[]> CreateWithValidInput()
        {
            yield return new object[]
            {
                "Test",
                "Test",
                "test@hotmail.com"
            };
        }

        public static IEnumerable<object[]> CreateWithEmptyFirstname()
        {
            yield return new object[]
            {
                "",
                "Test",
                "test@hotmail.com"
            };
        }


        public static IEnumerable<object[]> CreateFirstnameWithWhiteSpace()
        {
            yield return new object[]
            {
                " ",
                "Test",
                "test@hotmail.com"
            };
        }

        public static IEnumerable<object[]> CreateWithInvalidEmail()
        {
            yield return new object[]
            {
                "Test",
                "Test",
                "testhotmail.com"
            };
        }




        #endregion

    }
}
