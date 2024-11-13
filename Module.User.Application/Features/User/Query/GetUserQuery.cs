using MediatR;
using SharedKernel.Dto.Features.User.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Features.User.Query;

public sealed record GetUserQuery(Guid Id) : IRequest<GetUserResponse>;
