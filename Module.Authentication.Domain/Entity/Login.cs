﻿using System.Text.RegularExpressions;
using Module.Authentication.Domain.DomainServices.Interfaces;

namespace Module.Authentication.Domain.Entity;

public class Login
{
    public Guid Id { get; }
    public string Email { get; protected set; }
    public string PasswordHash { get; protected set; }

    protected Login()
    {
        
    }
    
    private Login(string email, string password, IPasswordHasher passwordHasher)
    {
        AssurePasswordCompliesWithRequirements(password);

        Email = email;
        PasswordHash = passwordHasher.Hash(password);
    }

    public static Login Create(string email, string password, IPasswordHasher passwordHasher)
    {
        return new Login(email, password, passwordHasher);
    }

    protected void AssurePasswordCompliesWithRequirements(string password)
    {
        if (password.Length < 10)
            throw new ArgumentException("Adgangskode skal være minimum 10 karaktere langt");
        if (password.All(c => !char.IsUpper(c)))
            throw new ArgumentException("Adgangskode skal have mindst ét stort bogstav");
        if (password.All(c => !char.IsNumber(c)))
            throw new ArgumentException("Adgangskode skal have mindst ét tal");
        
        var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
        if (regexItem.IsMatch(password))
            throw new ArgumentException("Adgangskoden skal have mindst ét specialtegn");
    }
}