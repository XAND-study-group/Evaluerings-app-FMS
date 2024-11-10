using MediatR;
using Module.User.Application.Features.UserManagement.Query.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Features.UserManagement.Query;

public sealed record GetUsersQuery() : IRequest<IEnumerable<UserResponse>>;
