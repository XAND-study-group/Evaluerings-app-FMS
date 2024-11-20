namespace SharedKernel.Dto.Features.School.User.Command;

public record CreateUserRequest(
    string Firstname,
    string Lastname,
    string Email,
    string Password);