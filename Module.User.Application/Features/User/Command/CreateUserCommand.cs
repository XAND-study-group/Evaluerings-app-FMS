using MediatR;
using Module.Shared.Abstractions;
using Module.User.Application.Abstractions;
using Module.User.Domain.Entities;
using SharedKernel.Dto.Features.User.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.Shared.Models;

namespace Module.User.Application.Features.User.Command
{
    public record CreateUserCommand(CreateUserRequest Request) : IRequest<Result<bool>>, ITransactionalCommand;
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRequest = request.Request;

                // Create 
                var user = Domain.Entities.User.Create(userRequest.Firstname, userRequest.Lastname, userRequest.Email);

                // Do & Save 
                await _userRepository.CreateUserAsync(user);
                return Result<bool>.Create("Brugeren er blevet oprettet.", true, ResultStatus.Created);
            }
            catch (ArgumentException e)
            {
                return Result<bool>.Create(e.Message, true, ResultStatus.Created);
            }
        }
    }
}
