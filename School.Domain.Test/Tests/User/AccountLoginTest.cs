using System.Reflection;
using Moq;
using School.Domain.DomainServices.Interfaces;
using School.Domain.Entities;
using SharedKernel.Enums.Features.Authentication;
using Xunit;

namespace School.Domain.Test.Tests.User;

public class AccountLoginTest
{
    [Theory]
    [MemberData(nameof(InvalidPasswordData))]
    public void Create_ShouldThrowArgumentException_WhenInvalidPassword(string password, string expectedMessage)
    {
        // Arrange
        var email = "test@example.com";
        var passwordHasherMock = new Mock<IPasswordHasher>();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => AccountLogin.Create(email, password, It.IsAny<Domain.Entities.User>(), It.IsAny<Role>(), passwordHasherMock.Object));
        Assert.Equal(expectedMessage, exception.Message);
    }
    
    [Theory]
    [MemberData(nameof(ValidCreateLoginsData))]
    public void Create_ShouldNotThrowExceptions_WhenValidDataIsGiven(string email, string password)
    {
        // Arrange
        var passwordHasherMock = new Mock<IPasswordHasher>();
        passwordHasherMock.Setup(mock => mock.Hash(It.IsAny<string>())).Returns(It.IsAny<string>());

        // Act & Assert
        Assert.NotNull(AccountLogin.Create(email, password, It.IsAny<Domain.Entities.User>(), It.IsAny<Role>(), passwordHasherMock.Object));
    }

    #region MemberData

    public static IEnumerable<object[]> InvalidPasswordData()
    {
        
        yield return ["short", "Adgangskode skal være minimum 10 karaktere langt"];
        yield return ["nouppercase1!", "Adgangskode skal have mindst ét stort bogstav"];
        yield return ["NoNumberInPassword!", "Adgangskode skal have mindst ét tal"];
        yield return ["NoSpecialChar1", "Adgangskoden skal have mindst ét specialtegn"];
    }
    
    public static IEnumerable<object[]> ValidCreateLoginsData()
    {
        var email = "test@example.com";
        
        yield return [email, "ValidPassword123!"];
        yield return [email, "OtherValidPassword98."];
        yield return [email, "SomeWeirdPassword45#"];
        yield return [email, "BestPassword5*"];
    }
    
    

    #endregion
    
}