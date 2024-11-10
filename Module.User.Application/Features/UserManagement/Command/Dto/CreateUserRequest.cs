namespace Module.User.Application.Features.UserManagement.Command.Dto;
public record CreateUserRequest(string Firstname,
                                string Lastname,
                                string Email);