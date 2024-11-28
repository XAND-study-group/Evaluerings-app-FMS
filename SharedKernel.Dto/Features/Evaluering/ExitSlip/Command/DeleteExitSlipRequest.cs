using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto.Features.Evaluering.ExitSlip.Command
{
    public record DeleteExitSlipRequest(
        Guid Id,
        byte[] RowVersion);    
}
