using MediatR;
using Module.Shared.Abstractions;
using Module.User.Application.Abstractions;
using Module.User.Application.Features.UserManagement.Command.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Features.UserManagement.Command
{
    public record CreateUserCommand(CreateUserRequest Request) : IRequest<Task>, ITransactionalCommand;
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Task>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Task> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userRequest = request.Request;

            // Create
            var user = Domain.Entity.User.Create(userRequest.Firstname, userRequest.Lastname, userRequest.Email);

            // Do & Save
            await _userRepository.CreateUserAsync(user);

            return Task.CompletedTask;
        }
    }
}
