using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.User.Command;

public record CreateUserRequest(
    string Firstname,
    string Lastname,
    string Email);