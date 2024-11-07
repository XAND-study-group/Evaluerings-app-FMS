﻿using Module.Authentication.Domain.DomainServices.Interfaces;
using Module.Authentication.Domain.Entity;
using Moq;
using Xunit;

namespace Module.Authentication.Domain.Test.Tests;

public class LoginTest
{
    [Theory]
    [MemberData(nameof(InvalidPasswordData))]
    public void Create_ShouldThrowArgumentException_WhenInvalidPassword(string password, string expectedMessage)
    {
        // Arrange
        var email = "test@example.com";
        var passwordHasherMock = new Mock<IPasswordHasher>();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => Login.Create(email, password, passwordHasherMock.Object));
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
        Assert.NotNull(Login.Create(email, password, passwordHasherMock.Object));
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