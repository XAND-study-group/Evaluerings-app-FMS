namespace SharedKernel.Dto.Features.Authentication.Command;

public record CreateAccountLoginRequest(
    string Username, 
    string Password, 
    string Email, 
    string Firstname, 
    string Lastname);