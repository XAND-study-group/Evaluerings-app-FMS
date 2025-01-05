using Moq;
using School.Domain.DomainServices.Interfaces;
using School.Domain.Test.Fakes.User;
using SharedKernel.Enums.Features.Authentication;
using Xunit;
using Assert = Xunit.Assert;

namespace School.Domain.Test.Tests.User;

public class UserTests
{
    #region Tests

    #region Create Tests

    [Theory]
    [InlineData("Xabur", "Zaradeshtø", "xabur@hotmail.com", "Password123.")]
    public void Given_valid_input_then_create_success(string firstname, string lastname, string email,
        string password)
    {
        // Arrange
        var domainServiceMock = new Mock<IUserDomainService>();
        domainServiceMock.Setup(mock => mock.DoesUserEmailExist(It.IsAny<string>()))
            .Returns(false);

        var accountClaimRepositoryMock = new Mock<IAccountClaimRepository>();
        accountClaimRepositoryMock
            .Setup(mock => mock.AddClaimsForRole(It.IsAny<Entities.User>(), It.IsAny<Role>()));

        // Act
        var user = Entities.User.Create(firstname, lastname, email, password, It.IsAny<Role>(),
            domainServiceMock.Object, accountClaimRepositoryMock.Object);

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
    public void Given_unique_email_then_void(string email, IEnumerable<string> otherEmails)
    {
        // Arrange
        var emails = otherEmails as string[] ?? otherEmails.ToArray();
        var userDomainServiceMock = new Mock<IUserDomainService>();
        userDomainServiceMock.Setup(domainService => domainService.DoesUserEmailExist(email))
            .Returns(!emails.Contains(email));

        // Act & Assert 
        Assert.True(userDomainServiceMock.Object.DoesUserEmailExist(email));
    }

    [Theory]
    [MemberData(nameof(NotUniqeEmails))]
    public async void Given_not_unique_email_then_throw_argumentException(string email,
        IEnumerable<string> otherEmails)
    {
        // Arrange
        //var emails = otherEmails as string[] ?? otherEmails.ToArray();
        var userDomainServiceMock = new Mock<IUserDomainService>();
        userDomainServiceMock.Setup(domainService => domainService.DoesUserEmailExist(email))
            .Returns(!otherEmails.Contains(email));

        // Act & Assert 
        Assert.False(userDomainServiceMock.Object.DoesUserEmailExist(email));
    }

    [Theory]
    [MemberData(nameof(InvalidEmailInput))]
    public void Given_invalid_email_format_then_throw_argumentException(string email)
    {
        // Arrange 
        var user = new FakeUser();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => user.SetUserEmail(email));
    }

    #endregion

    #region RefreshToken Tests

    [Theory]
    [MemberData(nameof(CreateRefreshTokenSuccess))]
    public void Given_Expiration_Date_In_Future_Then_Create(string firstname, string lastname, string email,
        DateTime expirationDate)
    {
        // Arrange
        var sut = new FakeUser(firstname, lastname, email);

        // Act & Assert
        Assert.NotNull(() => sut.AddRefreshToken(It.IsAny<string>(), expirationDate));
    }

    [Theory]
    [MemberData(nameof(CreateRefreshTokenFail))]
    public void Given_Expiration_Date_In_Past_Then_Throw_Exception(string firstname, string lastname, string email,
        int daysInPast)
    {
        // Arrange
        var sut = new FakeUser(firstname, lastname, email);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            sut.AddRefreshToken(It.IsAny<string>(), DateTime.Now.AddDays(-daysInPast)));
    }

    #endregion

    #endregion

    #region MemberData Methodes

    private static IEnumerable<string> GetOtherEmails()
    {
        return new[]
        {
            "Andreas@gmail.com",
            "Nikllerman@outlook.com",
            "Daniel@hotmail.com"
        };
    }


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
        yield return new object[]
        {
            "xabur"
        };

        yield return new object[]
        {
            "Xabur@d"
        };

        yield return new object[]
        {
            "alskdjasljdlasknflksnglkasdælkasflkanglkjsfæasjdæalsflakngalkjflksajflksajdlksajdlaksjdlakjfslaskjdlaskjflsakjdlaskjflsakjdalksjflaksjflaskjflaksjflaskfjalskfjlsakjflsakjflask" +
            "ælsakæsafkpasodkpasofkpsodkpofsaksapofsajaosdjpoaskdpsaodkpsaokdpsaokdpasokd@hotmail.com"
        };

        yield return new object[]
        {
            " "
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
            3
        ];
    }

    #endregion
}