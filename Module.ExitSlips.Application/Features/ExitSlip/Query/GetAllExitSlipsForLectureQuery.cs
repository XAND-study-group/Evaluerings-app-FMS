using MediatR;
using SharedKernel.Dto.Features.Evaluering.ExitSlip.Query;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ExitSlip.Application.Features.ExitSlip.Query
{
    public sealed record GetAllExitSlipsForLectureQuery(Guid lectureId) : IRequest<Result<IEnumerable<GetSimpleExitSlipsResponse?>>>;
}
