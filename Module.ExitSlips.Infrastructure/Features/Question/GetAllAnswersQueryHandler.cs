using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.ExitSlip.Application.Features.Question.Query;
using Module.ExitSlip.Infrastructure.DbContexts;
using SharedKernel.Dto.Features.Evaluering.Question.Query;
using SharedKernel.Models;

namespace Module.ExitSlip.Infrastructure.Features.Question
{
    public class GetAllAnswersQueryHandler : IRequestHandler<GetAllAnswersQuery, Result<IEnumerable<GetAllAnswersResponse>>>
    {
        
    }
}
