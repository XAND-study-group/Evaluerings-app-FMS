namespace SharedKernel.Dto.Features.User.Command;

public record CreateUserRequest(
    string Firstname,
    string Lastname,
    string Email);