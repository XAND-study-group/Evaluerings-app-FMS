using Module.User.Domain.Test.Fakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Module.User.Domain.Test.Tests
{
    public class UserTests
    {
        #region Tests

        #region Create Tests

        [Fact]
        public void Given_valid_input_then_create_success()
        {
            // Arrange
            var firstname = "Xabur";
            var lastname = "Zaradeshtø";
            var email = "xabur@hotmail.com";

            // Act
            var user = Entities.User.Create(firstname, lastname, email, []);

            // Assert
            Assert.NotNull(user);
        }


        #endregion


        #region NameTests

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("151")]
        [InlineData("sar!-")]
        [InlineData("Andræs;")]
        [InlineData("andrea41")]

        public void Given_firstname_with_white_space_or_empty_then_throw_argumentException(string name)
        {
            // Arrange
            var user = new FakeUser();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => user.SetUserFirstname(" "));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("151")]
        [InlineData("sar!-")]
        [InlineData("Andræs;")]
        [InlineData("andrea41")]

        public void Given_lastname_with_white_space_or_empty_then_throw_argumentException(string name)
        {
            // Arrange
            var user = new FakeUser();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => user.SetUserLastname(" "));
        }
        #endregion


        #region EmailTests

        [Theory]
        [MemberData(nameof(UniqueEmails))]
        public void Given_uniqe_email_then_void(string email, IEnumerable<string> otherEmails)
        {
            // Arrange 
            var user = new FakeUser();

            // Act & Assert
            user.SetUserEmail(email, otherEmails);
        }

        [Theory]
        [MemberData(nameof(NotUniqeEmails))]
        public void Given_not_uniqe_email_then_throw_argumentException(string email, IEnumerable<string> otherEmails)
        {
            // Arrange
            var user = new FakeUser();

            // Act & Assert 
            Assert.Throws<ArgumentException>(() => user.SetUserEmail(email, otherEmails));
        }

        [Theory]
        [MemberData(nameof(InvalidEmailInput))]
        public void Given_invalid_email_format_then_throw_argumentException(string email, IEnumerable<string> otheremails)
        {
            // Arrange 
            var user = new FakeUser();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => user.SetUserEmail(email, otheremails));
        }


        #endregion

        #region RefreshToken Tests
        
        [Theory]
        [MemberData(nameof(CreateRefreshTokenSuccess))]
        public void Given_Expiration_Date_In_Future_Then_Create(string firstname, string lastname, string email, DateTime expirationDate)
        {
            // Arrange
            var sut = new FakeUser(firstname, lastname, email);

            // Act & Assert
            Assert.NotNull(() => sut.SetRefreshToken(It.IsAny<string>(), expirationDate));

        }
        
        [Theory]
        [MemberData(nameof(CreateRefreshTokenFail))]
        public void Given_Expiration_Date_In_Past_Then_Throw_Exception(string firstname, string lastname, string email, DateTime expirationDate)
        {
            // Arrange
            var sut = new FakeUser(firstname, lastname, email);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => sut.SetRefreshToken(It.IsAny<string>(), expirationDate));

        }

        #endregion

        #endregion

        #region MemberData Methodes

        private static IEnumerable<string> GetOtherEmails()
            => new[]
            {
                "Andreas@gmail.dk",
                "Nikllerman@outlook.com",
                "Daniel@hotmail.com"
            };


        public static IEnumerable<object[]> NotUniqeEmails()
        {
            var otherEmails = GetOtherEmails();
            yield return new object[]
            {
                "Andreas@gmail.com",
                otherEmails
            };
        }

        public static IEnumerable<object[]> UniqueEmails()
        {
            var otherEmails = GetOtherEmails();
            yield return new object[]
            {
                "Xabur@hotmail.com",
                otherEmails
            };
        }

        public static IEnumerable<object[]> InvalidEmailInput()
        {
            var otheremails = GetOtherEmails();

            yield return new object[]
            {
                "xabur",
                otheremails
            };

            yield return new object[]
            {
                "Xabur@d",
                otheremails
            };

            yield return new object[]
            {
                "alskdjasljdlasknflksnglkasdælkasflkanglkjsfæasjdæalsflakngalkjflksajflksajdlksajdlaksjdlakjfslaskjdlaskjflsakjdlaskjflsakjdalksjflaksjflaskjflaksjflaskfjalskfjlsakjflsakjflask" +
                "ælsakæsafkpasodkpasofkpsodkpofsaksapofsajaosdjpoaskdpsaodkpsaokdpsaokdpasokd@hotmail.com",
                otheremails
            };

            yield return new object[]
            {
                " ",
                otheremails
            };
        }
        public static IEnumerable<object[]> CreateRefreshTokenSuccess()
        {
            yield return
            [
                "Test",
                "Test",
                "testhotmail.com",
                DateTime.Now.AddDays(5)
            ];
        }
        
        public static IEnumerable<object[]> CreateRefreshTokenFail()
        {
            yield return
            [
                "Test",
                "Test",
                "testhotmail.com",
                DateTime.Now.Subtract(new DateTime(0, 0, 3))
            ];
        }


        #endregion


    }
}